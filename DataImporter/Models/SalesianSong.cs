using System.Runtime.Serialization;

namespace DataImporter.Models
{
    [DataContract(Name = "piesen")]
    public class SalesianSong
    {
        [DataMember(Name = "cislo")] 
        public string Number { get; set; }
        
        [DataMember(Name = "nazov")] 
        public string Title { get; set; }
        
        [DataMember(Name = "autori")] 
        public string Authors { get; set; }
        
        [DataMember(Name = "kategorie")] 
        public SalesianCategories Categories { get; set; }
        
        [DataMember(Name = "text")]
        public string Text { get; set; }
        
        [DataMember(Name="url")]
        public string Url { get; set; }
    }
}