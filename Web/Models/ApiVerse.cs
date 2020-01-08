using Database.Models.Songs;

namespace Web.Models
{
    public class ApiVerse
    {
        
        public long Id { get; set; }
        
        public int SequenceNumber { get; set; }
        
        public string Title { get; set; }
        
        public string Text { get; set; }

        public ApiVerse()
        {
            
        }
        
        public ApiVerse(Verse databaseVerse)
        {
            Id = databaseVerse.Id;
            SequenceNumber = databaseVerse.SequenceNumber;
            Title = databaseVerse.Title;
            Text = databaseVerse.Text;
        }

        public Verse ToDatabaseVerse(long songId)
        {
            return new Verse
            {
                Id = Id,
                SequenceNumber = SequenceNumber,
                Title = Title,
                Text = Text,
                SongId = songId
            };
        }
    }
}