using System;
using System.Linq;
using EKS_database_web.Data;
using Microsoft.EntityFrameworkCore;

namespace Database.Models.Songs
{
    public class SongsDbContext : DbContext
    {
        public SongsDbContext()
        {
        }

        public SongsDbContext(DbContextOptions<SongsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<SongCategory> SongCategories { get; set; }
        public virtual DbSet<SongPlaylist> SongPlaylists { get; set; }
        public virtual DbSet<Verse> Verses { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SongCategory>(entity =>
            {
                entity.HasKey(sc => new {sc.SongId, sc.CategoryId});

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SongCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.SongCategories)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SongPlaylist>(entity =>
            {
                entity.HasKey(sc => new {sc.SongId, sc.PlaylistId});

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.SongPlaylists)
                    .HasForeignKey(d => d.PlaylistId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.SongPlaylists)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public void ClearAll()
        {
            Categories.RemoveRange(Categories);
            Playlists.RemoveRange(Playlists);
            Songs.RemoveRange(Songs);
            SongCategories.RemoveRange(SongCategories);
            SongPlaylists.RemoveRange(SongPlaylists);
            Verses.RemoveRange(Verses);
        }

        public void UpdateSongWithVersesAndCategories(Song newSong)
        {
            if(newSong == null) return;
            
            var originalSong = Songs
                .Include(s => s.Verses)
                .Include(s => s.SongCategories)
                .FirstOrDefault(s => s.Id == newSong.Id);        // read updated song from database

            if (originalSong == null)                                    // song is not in database now
            {
                Songs.Add(newSong);
            }
            else                                                         // song is in database
            {
                originalSong.Number = newSong.Number;
                originalSong.Title = newSong.Title;
                originalSong.Author = newSong.Author;
                originalSong.LastModifiedDateTimeLocal = DateTime.Now.ToLocalTime();    // time of last edit update
                foreach (var verse in newSong.Verses)                        // update existing verses
                {
                    UpdateVerse(verse);
                }
                foreach (var songCategory in newSong.SongCategories)        // add new relations to categories 
                {
                    AddSongCategoryIfNot(songCategory);
                }
                
                foreach (var verse in originalSong.Verses)                    // remove, if some verse has been removed
                {
                    if (newSong.Verses.Any(v => v.Id == verse.Id) == false)
                    {
                        Verses.Remove(verse);
                    }
                }
                
                foreach (var songCategory in originalSong.SongCategories)    // remove, if some songcategory has been removed
                {
                    if (newSong.SongCategories.Any(sc => sc.CategoryId == songCategory.CategoryId) == false)
                    {
                        SongCategories.Remove(songCategory);
                    }
                }
            }
        }

        public void UpdatePlaylistWithSongs(Playlist newPlaylist)
        {
            if (newPlaylist == null) return;

            var originalPlaylist = Playlists
                .Include(p => p.SongPlaylists)
                .FirstOrDefault(p => p.Id == newPlaylist.Id);

            
            if (originalPlaylist == null)            // playlist now not exist
            {
                Playlists.Add(newPlaylist);    
            }
            else
            {
                originalPlaylist.Name = newPlaylist.Name;
                originalPlaylist.Description = newPlaylist.Description;
                originalPlaylist.LastModifiedDateTimeLocal = DateTime.Now.ToLocalTime();     // time of last edit update
                
                foreach (var songPlaylist in newPlaylist.SongPlaylists)                        // update existing verses
                {
                    AddSongPlaylistIfNot(songPlaylist);
                }
                
                foreach (var songPlaylist in originalPlaylist.SongPlaylists)    // remove, if some songplaylist has been removed
                {
                    if (newPlaylist.SongPlaylists.Any(sp => sp.SongId == songPlaylist.SongId) == false) // in new playlist isn't this songplaylist
                    {
                        SongPlaylists.Remove(songPlaylist);        // remove it
                    }
                }
            }
        }

        private void AddSongPlaylistIfNot(SongPlaylist songPlaylist)
        {
            if(songPlaylist == null)    return;

            if (SongPlaylists.Any(sp => sp.PlaylistId == songPlaylist.PlaylistId && sp.SongId == songPlaylist.SongId) == false)
            {
                SongPlaylists.Add(songPlaylist);
            }
        }

        private void AddSongCategoryIfNot(SongCategory songCategory)
        {
            if(songCategory == null)    return;
            
            var existing = SongCategories
                .FirstOrDefault(sc => sc.CategoryId == songCategory.CategoryId && sc.SongId == songCategory.SongId);

            if (existing == null)
            {
                SongCategories.Add(songCategory);
            }
        }

        private void UpdateVerse(Verse verse)
        {
            if(verse == null)    return;

            var originalVerse = Verses.FirstOrDefault(v => v.Id == verse.Id);

            if (originalVerse == null)
            {
                Verses.Add(verse);
            }
            else
            {
                originalVerse.Title = verse.Title;
                originalVerse.Text = verse.Text;
                originalVerse.SequenceNumber = verse.SequenceNumber;
            }
        }
    }
}
