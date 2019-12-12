using Database.Models.Songs;

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

        public bool Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}