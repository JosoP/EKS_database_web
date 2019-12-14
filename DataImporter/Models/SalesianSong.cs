using System;
using System.Runtime.Serialization;

namespace DataImporter.Models
{
    [KnownType(typeof(SalesianCategories))]
    [DataContract(Name = "piesen", Namespace = "")]
    public class SalesianSong
    {
        [DataMember(Name = "cislo", Order = 0)] 
        public string Number { get; set; }

        public int? NumberInt
        {
            get
            {
                if(Int32.TryParse(Number, out int numberInt))
                {
                    return numberInt;
                }
                return null;
            }
        }

        [DataMember(Name = "nazov", Order = 1)]
        public string Title { get; set; }
        
        [DataMember(Name = "autori", Order = 2)] 
        public string Authors { get; set; }
        
        [DataMember(Name = "kategorie", Order = 3)] 
        public SalesianCategories Categories { get; set; }
        
        [DataMember(Name = "text", Order = 4)]
        public string Text { get; set; }
        
        [DataMember(Name="url", Order = 5)]
        public string Url { get; set; }
    }
}