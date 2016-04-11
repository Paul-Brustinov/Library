using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class ConsoleView
    {
        public static void ShowBook(Book _Book){
          //  if (_Book.KeyWords.Count > 1) { 
                Console.WriteLine("Наименование: {0}", _Book.Name);

                Console.WriteLine("Авторы:");
                foreach (var a in _Book.Authors) {
                    Console.WriteLine("    {0}", a.Name);
                }

                Console.WriteLine("Жанры:");
                foreach (var g in _Book.Genres)
                {
                    Console.WriteLine("    {0}", g.Name);
                }

                Console.WriteLine("Ключевые слова:");
                foreach (var k in _Book.KeyWords)
                {
                    Console.WriteLine("    {0}", k.Name);
                }
                Console.WriteLine("Серия - {0}, № - {1}, файл - {2}.{3} [{4}], язык - [{5}]", _Book.Series, _Book.Number, _Book.FileName, _Book.FileExt, _Book.FileSize, _Book.Lang);
                Console.WriteLine("----------------------------------------------------");
           // }

        }
    }
}
