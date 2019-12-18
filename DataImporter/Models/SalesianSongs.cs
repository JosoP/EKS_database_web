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
        //public List<SalesianSong> Songs { get; set; }
//        public bool SaveToDatabase(SongsDbContext dbContext)
//        {
//
//            foreach (var salesianSong in this)
//            {
//                var categories = FindOrCreateCetegories(salesianSong.Categories, dbContext);
//
//                var newSong = new Song
//                {
//                    Number = salesianSong.NumberInt,
//                    Title = salesianSong.Title,
//                    Author = salesianSong.Authors,
//                    Verses = ParseTextToVerses(salesianSong.Text),
//                    LastModifiedDateTime = DateTime.Now
//                };
//                
//                foreach (var category in categories)
//                {
//                    var songCategory = new SongCategory();
//                    songCategory.Category = category;
//                    songCategory.Song = newSong;
//                   // dbContext.SongCategories.Add(songCategory);
//
//                    Console.WriteLine($"Created: {songCategory}");
//                    
//                    //newSong.SongCategories.Add(songCategory);
//                    category.SongCategories.Add(songCategory);
//                }
//
//                dbContext.Songs.Add(newSong);
//                Console.WriteLine($"Created: {newSong}");
//
//                dbContext.SaveChanges();
//            }
//
//            return true;
//        }
//
//        private List<Category> FindOrCreateCetegories(SalesianCategories salesianSongCategories, SongsDbContext dbContext)
//        {
//            var categoriesToRet = new List<Category>();
//            foreach (var salesianCategory in salesianSongCategories)
//            {
//                var songCat = dbContext.Categories.FirstOr(cat => cat.Name == salesianCategory, null);
//                if (songCat == null)
//                {
//                    songCat = new Category
//                    {
//                        Name = salesianCategory
//                    };
//                    dbContext.Categories.Add(songCat);
//                    Console.WriteLine($"Created: \"{songCat}\"");
//                }
//
//                categoriesToRet.Add(songCat);
//            }
//
//            return categoriesToRet;
//        }


    }
}