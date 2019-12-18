using System.Collections.Generic;
using DataImporter.Models;

namespace DataImporter.Commands
{
    public interface IExecutable
    {
        bool Execute(List<UniversalSong> songs);
    }
}