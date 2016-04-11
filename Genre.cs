using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Genre:Tag {
        public Genre(string _Name) : base(_Name) { }
        public Genre(int _ID, string _Name) : base(_ID, _Name) { }
    }
}
