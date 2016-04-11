using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            //ContentReader cr = new ContentReader();

            //ContentReader.
            Library Lib = new Library();
            Console.WriteLine("Подождите, идет загрузка книг из БД...");
            MSSQL_Serialisation.LoadLibrary(Lib);

            InputController IC = new InputController(Lib);
            string s;
            do
            {
                s = Console.ReadLine();
            } while (IC.Control(s));


        }
    }


}
