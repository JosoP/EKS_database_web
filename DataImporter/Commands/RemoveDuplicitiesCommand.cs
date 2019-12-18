using System.Collections.Generic;
using Database.Models.Songs;
using DataImporter.Models;

namespace DataImporter.Commands
{
    public class RemoveDuplicitiesCommand : IExecutable
    {
        public RemoveDuplicitiesCommand()
        {
            
        }

        public RemoveDuplicitiesCommand(DuplicityType duplicityType, SongsDbContext dbContext)
        {
            throw new System.NotImplementedException();
        }

        public bool Execute(List<UniversalSong> songs)
        {
            throw new System.NotImplementedException();
        }
    }
}