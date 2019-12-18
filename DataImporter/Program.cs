using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Database.Models.Songs;
using DataImporter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            var isOk = true;
            
            var argumentParser = new ArgumentParser();
            argumentParser.Parse(args);

            if (argumentParser.AreAttributesCorrect)
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