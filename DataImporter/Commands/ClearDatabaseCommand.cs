using System;
using Database.Models.Songs;

namespace DataImporter.Commands
{
    public class ClearDatabaseCommand : IExecutable
    {
        private readonly SongsDbContext _dbContext;

        public ClearDatabaseCommand(SongsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Execute()
        {
            Console.WriteLine("Clearing of database started...");
            _dbContext.ClearAll();
            return true;
        }
    }
}