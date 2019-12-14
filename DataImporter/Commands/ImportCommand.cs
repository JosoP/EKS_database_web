using System;
using Database.Models.Songs;
using DataImporter.Importers;

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

        public bool Execute()
        {
            var importedSongs = _importer.Import();
            
            if (importedSongs != null)
            {
                return importedSongs.SaveToDatabase(_dbContext);
            }
            else
            {
                Console.WriteLine($"Importing of songs failed.");
                return false;
            }
        }
    }
}