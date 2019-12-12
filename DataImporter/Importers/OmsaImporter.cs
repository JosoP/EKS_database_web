using Database.Models.Songs;
using DataImporter.Models;

namespace DataImporter.Importers
{
    public class OmsaImporter : IImporter
    {
        public OmsaImporter(SongsDbContext dbContext)
        {
            throw new System.NotImplementedException();
        }

        public string Path { get; set; }

        public IImportedSongs Import()
        {
            throw new System.NotImplementedException();
        }
    }
}