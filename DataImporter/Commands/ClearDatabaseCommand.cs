using System;
using System.Collections.Generic;
using Database.Models.Songs;
using DataImporter.Models;

namespace DataImporter.Commands
{
    public class ClearDatabaseCommand : IExecutable
    {
        private readonly SongsDbContext _dbContext;

        public ClearDatabaseCommand(SongsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Execute(List<UniversalSong> songs)
        {
            Console.WriteLine("Clearing of database started...");
            _dbContext.ClearAll();
            _dbContext.SaveChanges();
            Console.WriteLine("Clearing of database done.");
            return true;
        }
    }
}