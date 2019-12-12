using DataImporter.Models;

namespace DataImporter.Importers
{
    public interface IImporter
    {
        string Path { get; set; }
        IImportedSongs Import();
    }
}