using System.Collections.Generic;
using DataImporter.Models;

namespace DataImporter.Importers
{
    public class OmsaImporter : Importer
    {
        public string Path { get; set; }

        public OmsaImporter()
        {
        }

        public override List<UniversalSong> Import()
        {
            throw new System.NotImplementedException();
        }

        public override bool ParseArguments(List<string> arguments)
        {
            throw new System.NotImplementedException();
        }
    }
}