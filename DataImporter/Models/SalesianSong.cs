using System;
using System.Collections.Generic;
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
                if (Int32.TryParse(Number, out int numberInt))
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

        [DataMember(Name = "text", Order = 4)] public string Text { get; set; }

        [DataMember(Name = "url", Order = 5)] public string Url { get; set; }

        public UniversalSong ToUniversal()
        {
            var universalCategories = new List<UniversalCategory>();

            foreach (var category in Categories)
            {
                universalCategories.Add(new UniversalCategory {Name = category});
            }

            return new UniversalSong
            {
                Title = this.Title,
                Number = NumberInt,
                Authors = Authors,
                Categories = universalCategories,
                Url = Url,
                Verses = ParseTextToVerses(),
                CreationDate = DateTime.Now
            };
        }


        private List<UniversalVerse> ParseTextToVerses()
        {
            const string proxyEnter = "[]";
            const string proxyVerseEnd = "[end]";

            var verses = new List<UniversalVerse>();
            if (Text != null)
            {
                if (Text.Length > 0)
                {
//                    var textWithoutChords = string.Join("", text.Split(']')
//                        .Select(p => p.Split('[')[0].Trim()));


                    var tempText = Text;
                    tempText = tempText.RemoveBetweenIncluding('[', ']') // remove chords
                        .RemoveBetweenIncluding('(', ')')
                        .Trim()
                        .Replace("\n\n\n", "\n")
                        .Replace("\n\n", "\n")
                        .Replace("\n&nbsp;&nbsp;&nbsp;&nbsp;", proxyEnter) // enter proxy
                        .Replace("&nbsp;", "")
                        .Trim()
                        .Replace("\n", proxyVerseEnd)
                        .Replace(proxyEnter, "\n");

                    tempText = tempText.Trim();
                    var verseTexts = tempText.Split(proxyVerseEnd);

                    foreach (var verseText in verseTexts)
                    {
                        if (verseText.Length > 0)
                        {
                            if (verseText.Contains(' '))
                            {
                                verses.Add(new UniversalVerse
                                {
                                    Title = verseText.Substring(0, verseText.IndexOf(' ')),
                                    Text = verseText.Substring(verseText.IndexOf(' ') + 1)
                                });
                            }
                            else
                            {
                                verses.Add(new UniversalVerse
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