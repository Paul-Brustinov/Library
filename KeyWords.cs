using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class KeyWord:Tag
    {
        public KeyWord(string _Name) : base(_Name) { }
        public KeyWord(int _ID, string _Name) : base(_ID, _Name) { }

    }
}
