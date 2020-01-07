using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataImporter.Models
{
    [KnownType(typeof(SalesianSong))]
    [CollectionDataContract(Name = "piesne", Namespace = "")]
    public class SalesianSongs : List<SalesianSong>
    {
    }
}