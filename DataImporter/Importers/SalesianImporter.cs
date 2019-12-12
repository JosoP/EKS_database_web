using DataImporter.Models;

namespace DataImporter.Importers
{
    public class SalesianImporter : IImporter
    {
        public string Path { get; set; }

        public IImportedSongs Import()
        {
            throw new System.NotImplementedException();
        }
    }
}