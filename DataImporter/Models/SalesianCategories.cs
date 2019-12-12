using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataImporter.Models
{
    [DataContract(Name="kategorie")]
    public class SalesianCategories
    {
        [DataMember(Name="kategoria")]
        public List<string> Categories { get; set; }
    }
}