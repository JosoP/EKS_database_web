using DataImporter.Importers;

namespace DataImporter.Commands
{
    public class ImportCommand : IExecutable
    {
        public ImportCommand(IImporter importer)
        {
            
        }

        public bool Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}