using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public delegate void StringProcessor(string[] s);
    class InputController
    {
        public Library Lib { get; set; }

        Dictionary<string, StringProcessor> Menu;

        public InputController(Library _Lib) {
            Lib = _Lib;
            Menu = new Dictionary<string, StringProcessor>();
            Menu.Add("fa", FindByAuthor);
            Menu.Add("fn", FindByName);
            Console.WriteLine("Введите FA <текст> - для поиска книг по автору:");
            Console.WriteLine("Введите FN <текст> - для поиска книг по наименованию:");
            WriteCommandLineBeginer();
        }
        

        public void FindByAuthor(string[] s) {
            List<Book> Books = Finder.FindByAuthor(Lib, s[1]);
            foreach (Book b in Books)
            {
                ConsoleView.ShowBook(b);
            }
            WriteCommandLineBeginer();
        }


        public void FindByName(string[] s)
        {
            List<Book> Books = Finder.FindByName(Lib, s[1]);
            foreach (Book b in Books)
            {
                ConsoleView.ShowBook(b);
            }
            WriteCommandLineBeginer();
        }

        public bool Control(string s)
        {
            if (s == "exit") return false;
            string[] Command;
            Command = s.Split(' ');

            if (Menu.ContainsKey(Command[0].ToLower()) && Command.Count() > 1  )
            {
                Menu[Command[0].ToLower()].Invoke(Command);
            }
            else 
            {
                Console.WriteLine("Неопознанная команда!");
                WriteCommandLineBeginer();
            }

            return true;
        }

        static void WriteCommandLineBeginer()
        {
            Console.Write(">");
        }


        //List<Book> Books = Finder.FindByName(Lib, "аксенов");
        //foreach (Book b in Books)
        //{
        //    ConsoleView.ShowBook(b);
        //}


    }
}
