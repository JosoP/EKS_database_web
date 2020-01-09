using System;
using System.Collections.Generic;
using DataImporter.Models;


namespace DataImporter.Commands
{
    /// <summary>
    ///     Class representing command in abstract layer. When new child of this class is creating, it have to be
    ///     registered in static constructor of this class.
    /// </summary>
    public abstract class Command
    {
        // list in which all commands have to be registered.
        private static readonly Dictionary<string, Type> AllCommands;

        /// <summary>
        ///     In this static constructor all child commands have to be registered in pair with its representing string
        ///     to the static list "AllCommands"
        /// </summary>
        static Command()
        {
            AllCommands = new Dictionary<string, Type>();

            AllCommands.Add("-Export", typeof(ExportCommand));
            AllCommands.Add("-Import", typeof(ImportCommand));
            AllCommands.Add("-RemoveDuplicities", typeof(RemoveDuplicitiesCommand));
        }

        /// <summary>
        ///     Executes operations specified by specific child command. Have to be implemented in child class.
        /// </summary>
        /// <param name="songs">List of songs on which command will be applied.</param>
        /// <returns>true when command has been successfully executed, otherwise false.</returns>
        public abstract bool Execute(List<UniversalSong> songs);

        /// <summary>
        ///     Parses arguments specific for child command. Have to be implemented in child class.
        /// </summary>
        /// <param name="arguments">Arguments to be parsed.</param>
        /// <returns>true when arguments has been parsed correctly, otherwise false.</returns>
        public abstract bool ParseArguments(List<string> arguments);

        /// <summary>
        ///     Static method that returns instance of command specified by command string. All commands have to be
        ///     registered with its command name in static list "AllCommands" in this class.
        /// </summary>
        /// <param name="commandString">String representing command in application arguments.</param>
        /// <returns>Instance of found command, or null if no instance has been found.</returns>
        public static Command FindCommand(string commandString)
        {
            foreach (var command in AllCommands)
            {
                if (command.Key == commandString)
                {
                    return (Command) Activator.CreateInstance(command.Value);
                }
            }

            return null;
        }
    }
}