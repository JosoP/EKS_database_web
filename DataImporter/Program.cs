using System;
using System.Collections.Generic;
using DataImporter.Models;

namespace DataImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            var isOk = true;

            var argumentParser = new ArgumentParser();

            if (argumentParser.Parse(args))
            {
                var editedSongs = new List<UniversalSong>();
                var commandIndex = 0;

                foreach (var command in argumentParser.Commands)
                {
                    Console.WriteLine($"{++commandIndex}. command");

                    var isSuccess = command.Execute(editedSongs);
                    if (isSuccess == false)
                    {
                        isOk = false;
                        break;
                    }

                    Console.WriteLine("--------------------------\n");
                }
            }
            else
            {
                Console.Error.WriteLine("Bad arguments.");
                isOk = false;
            }

            if (isOk)
            {
                Console.WriteLine("All commands are done successfully.");
            }
            else
            {
                Console.WriteLine("Some error occured during commands execution.");
            }
        }
    }
}