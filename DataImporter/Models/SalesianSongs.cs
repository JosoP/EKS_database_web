using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Database.Models.Songs;

namespace DataImporter.Models
{
    
    
    [KnownType(typeof(SalesianSong))]
    [CollectionDataContract(Name = "piesne", Namespace = "")]
    public class SalesianSongs : List<SalesianSong>, IImportedSongs
    {    
        //public List<SalesianSong> Songs { get; set; }

        private const string PROXY_ENTER = "[]"; 
        private const string PROXY_VERSE_END = "[end]"; 

        public List<Song> ToDatabaseSongs()
        {
            List<Song> databaseSongs = new List<Song>();
            foreach (var salesianSong in this)
            {
                //var categories = salesianSong.Categories.ToDatabaseCategories();
                databaseSongs.Add(new Song
                {
                    //Number = salesianSong.Number,
                    Title = salesianSong.Title,
                    Author = salesianSong.Authors,
                    Verses = ParseTextToVerses(salesianSong.Text),
                    LastModifiedDateTime = DateTime.Now,
                    
                });
            }

            return databaseSongs;
        }

        private List<Verse> ParseTextToVerses(string text)
        {
            List<Verse> verses = new List<Verse>();
            if (text != null)
            {
                if (text.Length > 0)
                {
                    
//                    var textWithoutChords = string.Join("", text.Split(']')
//                        .Select(p => p.Split('[')[0].Trim()));


                    var originalText = text;
                    text = text.RemoveBetweenIncluding('[', ']') // remove chords
                        .RemoveBetweenIncluding('(', ')')
                        .Trim()
                        .Replace("\n\n\n", "\n")
                        .Replace("\n\n", "\n")
                        .Replace("\n&nbsp;&nbsp;&nbsp;&nbsp;", PROXY_ENTER) // enter proxy
                        .Replace("&nbsp;", "")
                        .Trim()
                        .Replace("\n", PROXY_VERSE_END)
                        .Replace(PROXY_ENTER, "\n");

                    text = text.Trim();
                    var verseTexts = text.Split(PROXY_VERSE_END);
                    
                    foreach (var verseText in verseTexts)
                    {
                        if (verseText.Length > 0)
                        {
                            if (verseText.Contains(' '))
                            {
                                verses.Add(new Verse
                                {
                            
                                    Title = verseText.Substring(0, verseText.IndexOf(' ')),
                                    Text = verseText.Substring(verseText.IndexOf(' ') + 1)
                                });
                            }
                            else
                            {
                                verses.Add(new Verse
                                {
                            
                                    Title = "",
                                    Text = verseText
                                });
                            }
                                
                        }
                        else
                        {
                            Console.WriteLine("BAD VERSE... ");
                        }
                    }
                }
            }

            return verses;
        }

        
    }
}