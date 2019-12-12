using System.Collections.Generic;

namespace DataImporter
{
    public class ArgumentParser
    {
        public List<IExecutable> Commands { get; private set; }
        public bool AreAttributesCorrect { get; private set; }
        
        public ArgumentParser(string[] arguments )
        {
            AreAttributesCorrect = true;
            Commands = new List<IExecutable>();
        }
    }
}