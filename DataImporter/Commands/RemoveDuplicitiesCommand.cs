using System.Collections.Generic;
using Database.Models.Songs;
using DataImporter.Models;

namespace DataImporter.Commands
{
    public class RemoveDuplicitiesCommand : Command
    {
        private DuplicityType _duplicityType;
        
        public RemoveDuplicitiesCommand()
        {
            _duplicityType = DuplicityType.Unknown;
        }

        public override bool Execute(List<UniversalSong> songs)
        {
            throw new System.NotImplementedException();
        }
        
        public override bool ParseArguments(List<string> arguments)
        {
            if (arguments.Count != 1) return false;
            
            _duplicityType = arguments[0] switch
            {
                "SameName" => DuplicityType.SameName,
                "SameNumber" => DuplicityType.SameSongNumber,
                "SameData" => DuplicityType.SameData,
                _ => DuplicityType.Unknown
            };

            return _duplicityType != DuplicityType.Unknown;
        }
    }
}