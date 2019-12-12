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
        private SongsDbContext _dbContext;
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
                switch (arguments[i])
                {
                    case "-import":
                        if (arguments.Length > (i + 2))
                        {
                            var importerString = arguments[++i];
                            var path = arguments[++i];
                            var importer = GetImporter(importerString);
                            
                            if (importer != null && path != null)
                            {
                                importer.Path = path;
                                Commands.Add(new ImportCommand(importer));
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
                    
                    case "-clearDatabase":
                        Commands.Add(new ClearDatabaseCommand(_dbContext));
                        break;
                    
                    case "-removeDuplicities":
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
                        AreAttributesCorrect = false;
                        break;
                };
            }
        }

        private IImporter GetImporter(string importerName)
        {
            switch (importerName)
            {
                case "SalesianImporter":
                    return new SalesianImporter(_dbContext);
                case "OmsaImporter":
                    return new OmsaImporter(_dbContext);
            }

            return null;
        }

        private DuplicityType GetDuplicityType(string duplicityName)
        {
            switch (duplicityName)
            {
                case "SameName": return DuplicityType.SameName;
                case "SameNumber": return DuplicityType.SameSongNumber;
                case "SameData": return DuplicityType.SameData;
                default : return DuplicityType.Unknown;
            }
        }
    }
}