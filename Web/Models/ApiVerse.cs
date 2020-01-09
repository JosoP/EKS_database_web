using Database.Models.Songs;

namespace Web.Models
{
    /// <summary>
    ///     API format of verse class. This class is used for sending verse in API.
    /// </summary>
    public class ApiVerse
    {
        
        public long Id { get; set; }
        
        public int SequenceNumber { get; set; }
        
        public string Title { get; set; }
        
        public string Text { get; set; }

        public ApiVerse()
        {
            
        }
        
        /// <summary>
        ///     Constructor for building of instance of this class from its database equivalent. 
        /// </summary>
        /// <param name="databaseVerse">Database equivalent to this class.</param>
        public ApiVerse(Verse databaseVerse)
        {
            Id = databaseVerse.Id;
            SequenceNumber = databaseVerse.SequenceNumber;
            Title = databaseVerse.Title;
            Text = databaseVerse.Text;
        }

        /// <summary>
        ///     Converts instance of this class to instance of database equivalent class.
        /// </summary>
        /// <returns>Instance of database equivalent of this class.</returns>
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