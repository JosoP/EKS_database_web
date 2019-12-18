using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Database.Models.Songs;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataImporter.Models
{
    
    
    [KnownType(typeof(SalesianSong))]
    [CollectionDataContract(Name = "piesne", Namespace = "")]
    public class SalesianSongs : List<SalesianSong>
    {
    }
}