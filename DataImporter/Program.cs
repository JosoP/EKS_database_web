using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Database.Models.Songs;
using DataImporter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            var isOk = true;
            var configuration = GetAppConfiguration();
            var optionsBuilder = new DbContextOptionsBuilder<SongsDbContext>();
            optionsBuilder.UseSqlite(configuration.GetConnectionString("SongsDbConnection"));

            using var dbContext = new SongsDbContext(optionsBuilder.Options);
            var argumentParser = new ArgumentParser(dbContext);
            argumentParser.Parse(args);

            if (argumentParser.AreAttributesCorrect)
            {
                var editedSongs = new List<UniversalSong>();
                    
                foreach (var command in argumentParser.Commands)
                {
                    var isSuccess = command.Execute(editedSongs);
                    if (isSuccess == false)
                    {
                        isOk = false;
                        break;
                    }
                }
            }
            else
            {
                Console.Error.WriteLine("Bad arguments.");
                isOk = false;
            }

//                if (isOk)
//                {
//                    Console.WriteLine("Saving of database ...");
//                    //dbContext.SaveChanges();
//                }
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