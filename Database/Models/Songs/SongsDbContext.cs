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

        public void UpdateSong(Song song)
        {
            if(song == null) return;
            
            var originalSong = Songs
                .Include(s => s.Verses)
                .FirstOrDefault(s => s.Id == song.Id);

            if (originalSong == null)
            {
                Songs.Add(song);
            }
            else
            {
                originalSong.Number = song.Number;
                originalSong.Title = song.Title;
                originalSong.Author = song.Author;
                originalSong.LastModifiedDateTimeLocal = DateTime.Now.ToLocalTime();
                foreach (var verse in song.Verses)
                {
                    UpdateVerse(verse);
                }
                
                foreach (var verse in originalSong.Verses)    // if some song has been removed
                {
                    if (song.Verses.Any(v => v.Id == verse.Id) == false)
                    {
                        Verses.Remove(verse);
                    }
                }
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
