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
        public bool AreAttributesCorrect { get; private set; }
        
        public ArgumentParser()
        {
            AreAttributesCorrect = true;
            Commands = new List<Command>();
        }

        public void Parse(string[] arguments)
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
                        AreAttributesCorrect = false;
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
        }


    }
}