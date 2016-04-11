using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Author:Tag
    {
        public Author(string _Name) : base(_Name) { }
        public Author(int _ID, string _Name) : base(_ID, _Name) { }
    }
}
