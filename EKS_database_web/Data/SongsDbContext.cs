using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EKS_database_web.Data
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
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<SongCategory>(entity =>
            {
                entity.HasNoKey();

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
                entity.HasNoKey();

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.SongPlaylists)
                    .HasForeignKey(d => d.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.SongPlaylists)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Verse>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Title).HasDefaultValueSql("'strofa'");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.Vers)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
