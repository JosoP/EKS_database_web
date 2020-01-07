using System;
using System.Collections.Generic;
using DataImporter.Models;

namespace DataImporter.Importers
{
    public abstract class Importer
    {
        private static Dictionary<string, Type> _allImporters;

        static Importer()
        {
            _allImporters = new Dictionary<string, Type>();

            _allImporters.Add("SalesianData", typeof(SalesianImporter));
            _allImporters.Add("OmsaData", typeof(OmsaImporter));
        }

        public static Importer FindImporter(string importerString)
        {
            foreach (var importer in _allImporters)
            {
                if (importer.Key == importerString)
                {
                    return (Importer) Activator.CreateInstance(importer.Value);
                }
            }

            return null;
        }


        public abstract List<UniversalSong> Import();

        public abstract bool ParseArguments(List<string> arguments);
    }
}