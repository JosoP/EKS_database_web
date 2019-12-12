using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataImporter.Models
{
[DataContract(Name= "piesne")]
    public class SalesianSongs
    {    
        [DataMember(Name= "piesen")]
        public List<SalesianSong> Songs { get; set; }       
    }
}