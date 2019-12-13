using System.Collections.Generic;
using Database.Models.Songs;

namespace DataImporter.Models
{
    public interface IImportedSongs
    {
        List<Song> ToDatabaseSongs();
    }
}