using System;
using System.Collections.Generic;
using DataImporter.Models;


namespace DataImporter.Commands
{
    public abstract class Command
    {
        private static Dictionary<string, Type> allCommands;

        static Command()
        {
            allCommands = new Dictionary<string, Type>();
            
            allCommands.Add("-Export", typeof(ExportCommand));
            allCommands.Add("-Import", typeof(ImportCommand));
            allCommands.Add("-RemoveDuplicities", typeof(RemoveDuplicitiesCommand));
        }
        
        public abstract bool Execute(List<UniversalSong> songs);

        public abstract bool ParseArguments(List<string> arguments);

        public static Command FindCommand(string commandString)
        {
            foreach (var command in allCommands)
            {
                if (command.Key == commandString)
                {
                    return (Command) Activator.CreateInstance(command.Value);
                }
            }
            
            return null;
        }

//        public static void RegisterCommand(string commandString, Type command)
//        {
//            allCommands.Add(commandString, command);
//        }
    }
    
    
}