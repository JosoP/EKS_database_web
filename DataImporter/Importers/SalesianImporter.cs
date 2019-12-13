using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Database.Models.Songs;
using DataImporter.Models;

namespace DataImporter.Importers
{
    public class SalesianImporter : IImporter
    {

        public string Path { get; set; }

        public IImportedSongs Import()
        {
            try
            {
                Console.WriteLine("Importing of salesians songs started.");
                SalesianSongs importedSongs;
                var serializer = new DataContractSerializer(typeof(SalesianSongs));
                
                using (var stream = new FileStream(Path, FileMode.Open))
                {
                    importedSongs = (SalesianSongs) serializer.ReadObject(stream);    // import songs
                }
                
                Console.WriteLine($"Salesians songs has been successfuly imported. Count {importedSongs.Count}");
                return importedSongs;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured while importing salesians songs.");
                Console.WriteLine(e);
                return null;
            }
            
        }
    }
}