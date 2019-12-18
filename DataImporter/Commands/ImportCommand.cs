using System;
using System.Collections.Generic;
using Database.Models.Songs;
using DataImporter.Importers;
using DataImporter.Models;

namespace DataImporter.Commands
{
    public class ImportCommand : IExecutable
    {
        private readonly IImporter _importer;
        private readonly SongsDbContext _dbContext;

        public ImportCommand(IImporter importer, SongsDbContext dbContext)
        {
            _importer = importer;
            _dbContext = dbContext;
        }

        public bool Execute(List<UniversalSong> songs)
        {
            var importedSongs = _importer.Import();
            
            if (importedSongs != null)
            {
                //return importedSongs.SaveToDatabase(_dbContext);
                songs.AddRange(importedSongs);
                return true;
            }
            else
            {
                Console.WriteLine($"Importing of songs failed.");
                return false;
            }
        }
    }
}