using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public abstract class Tag
    {
        public List<Book> Books;
        public string Name { get; set; }
        internal int _ID { get; set; }
        public int ID { get { return _ID; } }

        public Tag() : this(0, "") { }
        public Tag(string _Name) : this(0, _Name) { }

        public Tag(int nID, string _Name)
        {
            _ID = nID;
            Name = _Name;
            Books = new List<Book>();
        }

    }
}
