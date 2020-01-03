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

        public void UpdateSong(Song newSong)
        {
            if(newSong == null) return;
            
            var originalSong = Songs
                .Include(s => s.Verses)
                .Include(s => s.SongCategories)
                .FirstOrDefault(s => s.Id == newSong.Id);

            if (originalSong == null)
            {
                Songs.Add(newSong);
            }
            else
            {
                originalSong.Number = newSong.Number;
                originalSong.Title = newSong.Title;
                originalSong.Author = newSong.Author;
                originalSong.LastModifiedDateTimeLocal = DateTime.Now.ToLocalTime();
                foreach (var verse in newSong.Verses)
                {
                    UpdateVerse(verse);
                }
                foreach (var songCategory in newSong.SongCategories)
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
