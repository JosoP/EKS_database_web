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
        private readonly SongsDbContext _dbContext;
        public List<IExecutable> Commands { get; private set; }
        public bool AreAttributesCorrect { get; private set; }
        
        public ArgumentParser(SongsDbContext dbContext)
        {
            _dbContext = dbContext;
            AreAttributesCorrect = true;
            Commands = new List<IExecutable>();
        }

        public void Parse(string[] arguments)
        {
            for (var i = 0; i < arguments.Length; i++)
            {
                var currentArgument = arguments[i];
                switch (currentArgument)
                {
                    case "-Import":
                        if (arguments.Length > (i + 2))
                        {
                            var importerString = arguments[++i];
                            var path = arguments[++i];
                            var importer = GetImporter(importerString);
                            
                            if (importer != null && path != null)
                            {
                                importer.Path = path;
                                Commands.Add(new ImportCommand(importer, _dbContext));
                            }
                            else
                            {
                                AreAttributesCorrect = false;
                                Console.WriteLine("-Import - One of arguments is bad");
                            }
                        }
                        else
                        {
                            AreAttributesCorrect = false;
                            Console.WriteLine("-Import command needs next 2 arguments <Importer type> <Path to locality for importing>");
                        }
                        break;
                    
                    case "-ClearDatabase":
                        Commands.Add(new ClearDatabaseCommand(_dbContext));
                        break;
                    
                    case "-RemoveDuplicities":
                        if (arguments.Length > (i + 1))
                        {
                            var duplicityName = arguments[++i];
                            var duplicityType = GetDuplicityType(duplicityName);
                            
                            if (duplicityType != DuplicityType.Unknown)
                            {
                                Commands.Add(new RemoveDuplicitiesCommand(duplicityType, _dbContext));
                            }
                            else
                            {
                                AreAttributesCorrect = false;
                            }
                        }
                        else
                        {
                            AreAttributesCorrect = false;
                        }
                        break;
                    default:
                        Console.WriteLine($"Argument {currentArgument} is not known.");
                        AreAttributesCorrect = false;
                        break;
                };
            }
        }

        private static IImporter GetImporter(string importerName)
        {
            return importerName switch
            {
                "SalesianImporter" => (IImporter) new SalesianImporter(),
                "OmsaImporter" => new OmsaImporter(),
                _ => null
            };
        }

        private static DuplicityType GetDuplicityType(string duplicityName)
        {
            return duplicityName switch
            {
                "SameName" => DuplicityType.SameName,
                "SameNumber" => DuplicityType.SameSongNumber,
                "SameData" => DuplicityType.SameData,
                _ => DuplicityType.Unknown
            };
        }
    }
}