using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Library
    {

        public List<Book> Books { get; set; }
        public Dictionary<int, Series> IDSeries { get; set; }
        public Dictionary<int, Lang> IDLangs { get; set; }
        public Dictionary<int, Author> IDAuthors { get; set; }
        public Dictionary<int, Genre> IDGenres { get; set; }
        public Dictionary<int, KeyWord> IDKeyWords { get; set; }
        public Dictionary<int, Book> IDBooks { get; set; }

        public Library() {
            IDSeries = new Dictionary<int, Series>();
            IDLangs = new Dictionary<int, Lang>();

            IDAuthors = new Dictionary<int, Author>();
            IDGenres = new Dictionary<int, Genre>();
            IDKeyWords = new Dictionary<int, KeyWord>();

            IDBooks = new Dictionary<int, Book>();
            Books = new List<Book>();
        }

    }
}
