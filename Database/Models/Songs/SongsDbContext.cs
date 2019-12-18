﻿using EKS_database_web.Data;
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
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.SongCategories)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SongPlaylist>(entity =>
            {
                entity.HasKey(sc => new {sc.SongId, sc.PlaylistId});

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.SongPlaylists)
                    .HasForeignKey(d => d.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.SongPlaylists)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
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
    }
}