using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Library
{
    public class ContentReader
    {
        public string SourceInfo { get; set; }
        public ZipArchive archive { get; set; }

        public ContentReader() : this(@"SourceInfo") {}

        public List<Book> Books;

        public ContentReader(string SI)
        {
            Books = new List<Book>();
            string[] s = new string[14];
            SourceInfo = SI;
            DirectoryInfo dir = new DirectoryInfo(SourceInfo);
            int Num = 0, _ID = 0, _Rate = 0, _FileSize = 0;
            bool _Deleted = false;
            DateTime _Date = new DateTime();

            Book _Book;

            //MSSQL_Serialisation MsSql = new MSSQL_Serialisation();


            foreach (FileInfo fi in dir.GetFiles())
            {
                string[] lines = System.IO.File.ReadAllLines(SourceInfo + @"\" + fi.Name);
                foreach (var l in lines) {
                    s = l.Split((char)4);
                    if (s.Count<string>() >= 13) { 
                        Int32.TryParse(s[4], out Num);
                        Int32.TryParse(s[6], out _FileSize);
                        Int32.TryParse(s[7], out _ID);
                        Int32.TryParse(s[12], out _Rate);
                        DateTime.TryParse(s[10], out _Date);
                        _Book = new Book(s[0], s[1], s[2], s[3], Num, s[5], _FileSize, _ID, _Deleted, s[9], _Date, s[11], _Rate, s[13]);
                        Books.Add(_Book);
                        MSSQL_Serialisation.ImportBook(_Book);
                        //_Book.Save(MSSQL_Serialisation.Connection);

                        //ConsoleView.ShowBook(_Book);
                        
                    }
//                    foreach(var s in str){
                                                
                        //Console.WriteLine(s);
//                    }
                }
            
            }
        }

        

        
    }

            //    System.IO.Stream fs;
            //int a;
            //System.IO.File.Delete(@"\1\*.*");
            //foreach (var Entry in ZipFile.Open(PathName, ZipArchiveMode.Read, System.Text.Encoding.UTF8).Entries)
            //{
            //    Console.WriteLine(Entry.Name);
            //    Entry.ExtractToFile(@"1\" + Entry.Name);
            //}

}
