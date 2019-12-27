using System;
using System.Collections.Generic;
using System.Linq;
using Database.Models.Songs;
using DataImporter.Commands;
using DataImporter.Importers;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataImporter
{
    public class ArgumentParser
    {
        public List<Command> Commands { get; private set; }
        
        public ArgumentParser()
        {
            Commands = new List<Command>();
        }

        public bool Parse(string[] arguments)
        {
            List<string> previousCommandArguments = null;

            foreach (var currentArgument in arguments)
            {
                var command = Command.FindCommand(currentArgument);
                if (command == null)
                {
                    if (previousCommandArguments != null)
                    {
                        previousCommandArguments.Add(currentArgument);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (previousCommandArguments != null)
                    {
                        Commands.Last().ParseArguments(previousCommandArguments);
                    }

                    previousCommandArguments = new List<string>();
                    Commands.Add(command);
                }
            }

            if (previousCommandArguments != null)
            {
                Commands.Last().ParseArguments(previousCommandArguments);
            }

            return true;
        }


    }
}