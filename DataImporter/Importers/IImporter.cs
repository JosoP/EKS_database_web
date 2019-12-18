using System.Collections.Generic;
using DataImporter.Models;

namespace DataImporter.Importers
{
    public interface IImporter
    {
        string Path { get; set; }
        List<UniversalSong> Import();
    }
}