using System;
using System.Collections.Generic;
using DataImporter.Importers;
using DataImporter.Models;

namespace DataImporter.Exporters
{
    public abstract class Exporter
    {
        private static readonly Dictionary<string, Type> AllExporters;
        
        static Exporter()
        {
            AllExporters = new Dictionary<string, Type>();
            
            AllExporters.Add("InternalDatabase", typeof(InternalDatabaseExporter));
        }
        
        public static Exporter FindExporter(string exporterString)
        {
            foreach (var exporter in AllExporters)
            {
                if (exporter.Key == exporterString)
                {
                    return (Exporter) Activator.CreateInstance(exporter.Value);
                }
            }
            
            return null;
        }
        
        public abstract bool Export(List<UniversalSong> songs);
        
        public abstract bool ParseArguments(List<string> arguments);
    }
}