using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Library
{
    public class Book
    {
        internal int _ID { get; set; }
        public int ID { get { return _ID; } }
        public List<Genre> Genres { get; set; }
        public List<Author> Authors { get; set; }
        public string Name { get; set; }
        public Series Series { get; set; }
        public int Number { get; set; }
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public long FileSize { get; set; }
        public int FromID { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateAdd { get; set; }
        public Lang Lang { get; set; }
        public int Rate { get; set; }
        public List<KeyWord> KeyWords { get; set; }

        public Book(string _Authors, string _Genres, string _Name, string _Series, int _Number, string _FileName, int _FileSize, int _FromID, bool _Deleted, string _FileExt, DateTime _DateAdd, string _Lang, int _Rate, string _KeyWords)
        { 
            Genres = new List<Genre>();
            foreach(var g in _Genres.Split(':')){
                Genres.Add(new Genre(g.Trim()));
            }

            Authors = new List<Author>();
            foreach (var a in _Authors.Split(':'))
            {
                Authors.Add(new Author(a.Trim()));
            }

            KeyWords = new List<KeyWord>();
            foreach (var k in _KeyWords.Split(','))
            {
                KeyWords.Add(new KeyWord(k.Trim()));
            }
            

            Name = _Name;
            Series = new Series(_Series);
            Lang = new Lang(_Lang);
            Number = _Number;
            FileName = _FileName;
            FileExt = _FileExt;
            FileSize = _FileSize;
            FromID = _FromID;
            Deleted = _Deleted;

            if (DateTime.Compare(_DateAdd, new DateTime(1900, 1, 1)) > 0)
                DateAdd = _DateAdd;
            else
                DateAdd = new DateTime(1900, 1, 1);

            Rate = _Rate;
        }

        public Book() { }
        public Book(int mID, string _Name, Series _Series, int _Number, string _FileName, int _FileSize, int _Deleted, int _FromID, string _FileExt, DateTime _DateAdd, Lang _Lang, int _Rate) {
            _ID = mID;
            Name = _Name;
            Series = _Series;
            Lang = _Lang;
            Number = _Number;
            FileName = _FileName;
            FileExt = _FileExt;
            FileSize = _FileSize;
            FromID = _FromID;


            Deleted = !(_Deleted == 0);

            if (DateTime.Compare(_DateAdd, new DateTime(1900, 1, 1)) > 0)
                DateAdd = _DateAdd;
            else
                DateAdd = new DateTime(1900, 1, 1);

            Rate = _Rate;

            Genres = new List<Genre>();
            Authors = new List<Author>();
            KeyWords = new List<KeyWord>();
        }

        public void Save() { 

        }
    }
}
