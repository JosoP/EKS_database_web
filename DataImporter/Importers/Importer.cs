using System;
using System.Collections.Generic;
using DataImporter.Models;

namespace DataImporter.Importers
{
    /// <summary>
    ///     Class representing importer in abstract layer. When new child of this class is creating, it have to be
    ///     registered in static constructor of this class.
    /// </summary>
    public abstract class Importer
    {
        // list in which all child importers have to be registered.
        private static readonly Dictionary<string, Type> AllImporters;

        /// <summary>
        ///     In this static constructor all child importers have to be registered in pair with its representing string
        ///     to the static list "AllImporters"
        /// </summary>
        static Importer()
        {
            AllImporters = new Dictionary<string, Type>();

            AllImporters.Add("SalesianData", typeof(SalesianImporter));
            AllImporters.Add("OmsaData", typeof(OmsaImporter));
        }

        /// <summary>
        ///     Static method that returns instance of importer specified by importer representing string.
        ///     All importers have to be registered with its importer name in static list "AllImporters" in this class.
        /// </summary>
        /// <param name="importerString">String representing importer in application arguments.</param>
        /// <returns>Instance of found importer, or null if no instance has been found.</returns>
        public static Importer FindImporter(string importerString)
        {
            foreach (var importer in AllImporters)
            {
                if (importer.Key == importerString)
                {
                    return (Importer) Activator.CreateInstance(importer.Value);
                }
            }

            return null;
        }

        /// <summary>
        ///     Imports songs from import source specified by child importer. Have to be implemented in child class.
        /// </summary>
        /// <returns>List of imported songs.</returns>
        public abstract List<UniversalSong> Import();

        /// <summary>
        ///     Parses arguments specific for child importer. Have to be implemented in child class.
        /// </summary>
        /// <param name="arguments">Arguments to be parsed.</param>
        /// <returns>true when arguments has been parsed correctly, otherwise false.</returns>
        public abstract bool ParseArguments(List<string> arguments);
    }
}