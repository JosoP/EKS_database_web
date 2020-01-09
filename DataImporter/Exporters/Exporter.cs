using System;
using System.Collections.Generic;
using DataImporter.Models;

namespace DataImporter.Exporters
{
    /// <summary>
    ///     Class representing exporter in abstract layer. When new child of this class is creating, it have to be
    ///     registered in static constructor of this class.
    /// </summary>
    public abstract class Exporter
    {
        // list in which all child exporters have to be registered.
        private static readonly Dictionary<string, Type> AllExporters;

        /// <summary>
        ///     In this static constructor all child exporters have to be registered in pair with its representing string
        ///     to the static list "AllExporters"
        /// </summary>
        static Exporter()
        {
            AllExporters = new Dictionary<string, Type>();

            AllExporters.Add("InternalDatabase", typeof(InternalDatabaseExporter));
        }

        /// <summary>
        ///     Static method that returns instance of exporter specified by exporter representing string.
        ///     All exporters have to be registered with its exporter name in static list "AllExporters" in this class.
        /// </summary>
        /// <param name="exporterString">String representing exporter in application arguments.</param>
        /// <returns>Instance of found exporter, or null if no instance has been found.</returns>
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

        /// <summary>
        ///     Exports all songs specified by parameter to the source according to specific child exporter class.
        ///     Have to be implemented in child class.
        /// </summary>
        /// <param name="songs">List of songs to be exported.</param>
        /// <returns>true when exporting has been done correctly, otherwise false.</returns>
        public abstract bool Export(List<UniversalSong> songs);

        /// <summary>
        ///     Parses arguments specific for child exporter. Have to be implemented in child class.
        /// </summary>
        /// <param name="arguments">Arguments to be parsed.</param>
        /// <returns>true when arguments has been parsed correctly, otherwise false.</returns>
        public abstract bool ParseArguments(List<string> arguments);
    }
}