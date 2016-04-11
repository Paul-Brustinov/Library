using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class Finder
    {
        public static List<Book> FindByAuthor(Library Lib, string SearchStr) {
            List<Book> Books = new List<Book>();
            var Authors =
            from value in Lib.IDAuthors.Values
            where value.Name.IndexOf(SearchStr, StringComparison.OrdinalIgnoreCase) != -1
            select value;

            foreach (Author a in Authors) {
                foreach (Book b in a.Books) {
                    Books.Add(b);
                }
            }
            return Books;
        }

        public static List<Book> FindByName(Library Lib, string SearchStr)
        {
            List<Book> Books = new List<Book>();
            var _Books =
            from value in Lib.IDBooks.Values
            where value.Name.IndexOf(SearchStr, StringComparison.OrdinalIgnoreCase) != -1
            select value;

            foreach (Book b in _Books)
            {
                Books.Add(b);
            }

            return Books;
        }

    }
}
