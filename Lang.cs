using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Lang : Tag
    {
        public Lang(string _Name) : base(_Name) { }
        public Lang(int _ID, string _Name) : base(_ID, _Name) { }
    }
}
