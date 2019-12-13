using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataImporter.Models
{
    [CollectionDataContract(Name = "kategorie", ItemName = "kategoria", Namespace = "")]
    public class SalesianCategories : List<string>
    {
        //public List<string> Categories { get; set; }
    }
}