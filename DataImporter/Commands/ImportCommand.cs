using System;
using System.Collections.Generic;
using Database.Models.Songs;
using DataImporter.Importers;
using DataImporter.Models;

namespace DataImporter.Commands
{
    public class ImportCommand : Command
    {
        private  Importer _importer;
        
//        static ImportCommand()
//        {
//            Command.RegisterCommand("-Import", typeof(ImportCommand));
//        }
        public ImportCommand()
        {
        }

        public override bool Execute(List<UniversalSong> songs)
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

        public override bool ParseArguments(List<string> arguments)
        {
            if (arguments.Count < 1) return false;
            
            _importer = Importer.FindImporter(arguments[0]);
            if (_importer == null)
            {
                return false;
            }
            else
            {
                return _importer.ParseArguments(arguments.GetRange(1, arguments.Count - 1));
            }
        }
    }
}