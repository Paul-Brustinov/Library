using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Series:Tag
    {
        public Series(string _Name) : base(_Name) { }
        public Series(int _ID, string _Name) : base(_ID, _Name) { }

    }
}
