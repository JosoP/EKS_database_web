using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using DataImporter.Models;

namespace DataImporter.Importers
{
    public class SalesianImporter : Importer
    {
        public string Path { get; set; }

        public override List<UniversalSong> Import()
        {
            try
            {
                Console.WriteLine("Importing of salesians songs started.");
                SalesianSongs importedSongs;
                var serializer = new DataContractSerializer(typeof(SalesianSongs));

                using (var stream = new FileStream(Path, FileMode.Open))
                {
                    importedSongs = (SalesianSongs) serializer.ReadObject(stream); // import songs
                }

                Console.WriteLine($"Salesians songs has been successfuly imported. Count {importedSongs.Count}");
                return importedSongs.Select(song => song.ToUniversal()).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured while importing salesians songs.");
                Console.WriteLine(e);
                return null;
            }
        }

        public override bool ParseArguments(List<string> arguments)
        {
            if (arguments.Count != 1) return false;

            var fileName = arguments[0];

            if (!File.Exists(fileName)) return false;

            Path = fileName;
            return true;
        }
    }
}