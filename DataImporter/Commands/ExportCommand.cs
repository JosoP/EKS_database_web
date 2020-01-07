using System.Collections.Generic;
using DataImporter.Exporters;
using DataImporter.Models;

namespace DataImporter.Commands
{
    public class ExportCommand : Command
    {
        private Exporter _exporter;

        public override bool Execute(List<UniversalSong> songs)
        {
            return _exporter.Export(songs);
        }

        public override bool ParseArguments(List<string> arguments)
        {
            if (arguments.Count < 1) return false;

            _exporter = Exporter.FindExporter(arguments[0]);
            if (_exporter == null)
            {
                return false;
            }
            else
            {
                return _exporter.ParseArguments(arguments.GetRange(1, arguments.Count - 1));
            }
        }
    }
}