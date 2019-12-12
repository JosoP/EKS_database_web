using DataImporter.Models;

namespace DataImporter.Importers
{
    public class OmsaImporter : IImporter
    {
        public string Path { get; set; }

        public IImportedSongs Import()
        {
            throw new System.NotImplementedException();
        }
    }
}