using System.Collections.Generic;
using Database.Models.Songs;

namespace DataImporter.Models
{
    public interface IImportedSongs
    {
        bool SaveToDatabase(SongsDbContext dbContext);
    }
}