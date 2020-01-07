using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Database.Models.Songs;
using DataImporter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;

namespace DataImporter.Exporters
{
    public class InternalDatabaseExporter : Exporter
    {
        private bool _clearDatabase;
        private bool _verbose = false;

        public InternalDatabaseExporter()
        {
            _clearDatabase = false;
        }

        public override bool Export(List<UniversalSong> songs)
        {
            Console.WriteLine("Exporting to internal database starting...");

            var configuration = GetAppConfiguration();
            var optionsBuilder = new DbContextOptionsBuilder<SongsDbContext>();
            optionsBuilder.UseSqlite(configuration.GetConnectionString("SongsDbConnection"));

            try
            {
                using var dbContext = new SongsDbContext(optionsBuilder.Options);

                if (_clearDatabase)
                {
                    dbContext.ClearAll();
                    dbContext.SaveChanges();
                    Console.WriteLine("Database cleared.");
                }

                foreach (var universalSong in songs)
                {
                    int seqNum = 0;
                    var newSong = new Song
                    {
                        Number = universalSong.Number,
                        Title = universalSong.Title,
                        Author = universalSong.Authors,
                        Verses = universalSong.Verses.Select(verse => new Verse
                        {
                            Title = verse.Title,
                            Text = verse.Text,
                            SequenceNumber = seqNum++ // sequence number for exact order of verses
                        }).ToList(),
                        LastModifiedDateTimeLocal = DateTime.Now.ToLocalTime()
                    };

                    var categories = FindOrCreateCetegories(universalSong.Categories, dbContext);
                    foreach (var category in categories)
                    {
                        var songCategory = new SongCategory();
                        songCategory.Category = category;
                        songCategory.Song = newSong;

                        //Console.WriteLine($"Created: {songCategory}");

                        category.SongCategories.Add(songCategory);
                    }

                    dbContext.Songs.Add(newSong);
                    if (_verbose)
                    {
                        Console.WriteLine($"Created: {newSong}");
                    }

                    dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            Console.WriteLine($"All {songs.Count} exported to the internal database.");
            return true;
        }

        public override bool ParseArguments(List<string> arguments)
        {
            foreach (var argument in arguments)
            {
                switch (argument)
                {
                    case "ClearDatabase":
                        _clearDatabase = true;
                        break;
                    case "Verbose":
                        _verbose = true;
                        break;
                    default:
                        return false;
                }
            }

            return true;
        }

        private List<Category> FindOrCreateCetegories(List<UniversalCategory> universalCategories,
            SongsDbContext dbContext)
        {
            var categoriesToRet = new List<Category>();
            foreach (var universalCategory in universalCategories)
            {
                var songCat = dbContext.Categories.FirstOr(cat => cat.Name == universalCategory.Name, null);
                if (songCat == null)
                {
                    songCat = new Category
                    {
                        Name = universalCategory.Name,
                        Description = universalCategory.Description
                    };
                    dbContext.Categories.Add(songCat);
                    Console.WriteLine($"Created: \"{songCat}\"");
                }

                categoriesToRet.Add(songCat);
            }

            return categoriesToRet;
        }

        static IConfigurationRoot GetAppConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}