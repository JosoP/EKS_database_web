using System.Collections.Generic;
using System.Linq;
using DataImporter.Commands;

namespace DataImporter
{
    /// <summary>
    ///     Ensures parsing of program arguments and representing all actions that needs to be done by list the of
    ///     commands that should be executed sequentially. 
    /// </summary>
    public class ArgumentParser
    {
        /// <summary>
        ///     Commands that should be executed.
        /// </summary>
        public List<Command> Commands { get; private set; }

        public ArgumentParser()
        {
            Commands = new List<Command>();
        }

        /// <summary>
        ///     Parses list of program arguments and build the list of commands to be executed according to it.
        /// </summary>
        /// <param name="arguments">List of program arguments.</param>
        /// <returns>true when everything has been parsed correctly, otherwise false.</returns>
        public bool Parse(string[] arguments)
        {
            List<string> previousCommandArguments = null;

            foreach (var currentArgument in arguments)
            {
                var command = Command.FindCommand(currentArgument);
                if (command == null)    // string does not represents any command
                {
                    if (previousCommandArguments != null)
                    {
                        previousCommandArguments.Add(currentArgument);    // add string to list of arguments of last command
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (previousCommandArguments != null) // there was some arguments placed after last command
                    {
                        Commands.Last().ParseArguments(previousCommandArguments);    // send arguments to last command
                    }

                    previousCommandArguments = new List<string>();    // create new list of arguments 
                    Commands.Add(command);
                }
            }

            if (previousCommandArguments != null)
            {
                Commands.Last().ParseArguments(previousCommandArguments);     // send arguments to last command
            }

            return true;
        }
    }
}