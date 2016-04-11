using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;

namespace Library
{
    public static class MSSQL_Serialisation
    {
        public static SqlConnection Connection;

        static MSSQL_Serialisation()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library.Properties.Settings.LibraryConnectionString"].ConnectionString);
            Connection.Open();
        }

        /// <summary>
        /// Проходится по всем элементам коллекции и сохраняет их в БД
        /// Название хранимой процедуры передается, все хранимые процедуры принимают два параметра - Наименование и идентификатор книги
        /// </summary>
        /// <typeparam name="T"> Наследник типа Tag Автор, Ключевое слово, Жанр...)</typeparam>
        /// <param name="Coll"> Сохраняемая коллекция (Авторы, Ключевые слова, Жанры...) унаследованная от Tag </param>
        /// <param name="sp_name"> Наименование хранимой процедуры для сохранения элемента этой коллекции </param>
        /// <param name="book"> Книга, к которой эта коллекция относится </param>
        private static void ProcessCollection<T>(List<T> Coll, string sp_name, Book book) where T : Tag
        {
            for (int i = 0; i < Coll.Count(); i++) {
                if (Coll[i].ID == 0)  ProcessElement(Coll[i], sp_name, book);
            }
        }

        /// <summary>
        ///  Обработка одного элемента для процедуры ProcessCollection
        ///  Вынесена отдельно, чтобы можно было использовать для одиночных элементов - Язык и Серия
        /// </summary>
        /// <typeparam name="T">Наследник типа Tag Автор, Ключевое слово, Жанр...)</typeparam>
        /// <param name="Elem">Сохраняемый элемент (Автор, Ключевые слово, Жанр, Язык, Серия...) унаследованная от Tag</param>
        /// <param name="sp_name">Наименование хранимой процедуры для сохранения элемента этой коллекции </param>
        /// <param name="book">Книга, к которой этот элемент относится</param>
        private static void ProcessElement<T>(T Elem, string sp_name, Book book) where T:Tag {
            SqlCommand cmd = new SqlCommand(sp_name, Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Name", Elem.Name));
            cmd.Parameters.Add(new SqlParameter("@BookID", book.ID));
            using (SqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    Elem._ID = (int)rd["ID"];
                }
            }
        }
        /// <summary>
        /// Сохранение книги, все свойства кроме коллекций
        /// </summary>
        /// <param name="book"> Объект сохраняемой книги </param>
        private static void SetBook(Book book)
        {
            SqlCommand cmd = new SqlCommand("[api].[SetBookFromL]", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Name", book.Name));
            cmd.Parameters.Add(new SqlParameter("@BkSeries", book.Series.ID));
            cmd.Parameters.Add(new SqlParameter("@BkNo", book.Number));
            cmd.Parameters.Add(new SqlParameter("@BkFileName", book.FileName));
            cmd.Parameters.Add(new SqlParameter("@BkFileExt", book.FileExt));
            cmd.Parameters.Add(new SqlParameter("@BkSize", book.FileSize));
            cmd.Parameters.Add(new SqlParameter("@BkFromID", book.FromID));
            cmd.Parameters.Add(new SqlParameter("@BkAddDate", book.DateAdd));
            cmd.Parameters.Add(new SqlParameter("@BkLang", book.Lang.ID));
            cmd.Parameters.Add(new SqlParameter("@BkRate", book.Rate));

            using (SqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    book._ID = (int)rd["ID"];
                }
            }
        }
        
        /// <summary>
        ///  Процедура сохранения книги и всех связанніх с нею коллекций
        /// </summary>
        /// <param name="book"> Сохраняемя книга </param>
        public static void ImportBook(Book book)
        {
            ProcessElement(book.Series, "[api].[SetSeries]", book);
            ProcessElement(book.Lang, "[api].[SetLang]", book);
            SetBook(book);
            ProcessCollection(book.Authors , "[api].[SetAuthor]", book);
            ProcessCollection(book.Genres, "[api].[SetGenre]", book);
            ProcessCollection(book.KeyWords, "[api].[SetKeyword]", book);
        }


        public static void LoadLibrary(Library Lib) {
            LoadTags(Lib.IDSeries,   "[api].[LoadSeries]");
            LoadTags(Lib.IDLangs,    "[api].[LoadLangs]");
            LoadTags(Lib.IDAuthors,  "[api].[LoadAuthors]");
            LoadTags(Lib.IDGenres,   "[api].[LoadGenres]");
            LoadTags(Lib.IDKeyWords, "[api].[LoadKeyWords]");
            LoadBooksFromDb(Lib);
            LoadBookAuthor(Lib);
            LoadBookGenres(Lib);
            LoadBookKeywords(Lib);
        }

        #region join
        static void LoadBookAuthor(Library Lib) {
            SqlCommand cmd = new SqlCommand("[api].[LoadBookAuthors]", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            Author auth;
            Book book;
            using (SqlDataReader rd = cmd.ExecuteReader()){
                while (rd.Read()) {
                    if (Lib.IDAuthors.TryGetValue((int)rd["TAG_ID"], out auth)) { 
                        if(Lib.IDBooks.TryGetValue((int)rd["BK_ID"], out book)){
                            book.Authors.Add(auth);
                            auth.Books.Add(book);
                        }
                    }
                }
            }
        }


        static void LoadBookGenres(Library Lib)
        {
            SqlCommand cmd = new SqlCommand("[api].[LoadBookGenres]", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            Genre genre;
            Book book;
            using (SqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    if (Lib.IDGenres.TryGetValue((int)rd["TAG_ID"], out genre))
                    {
                        if (Lib.IDBooks.TryGetValue((int)rd["BK_ID"], out book))
                        {
                            book.Genres.Add(genre);
                            genre.Books.Add(book);
                        }
                    }
                }
            }
        }

        static void LoadBookKeywords(Library Lib)
        {
            SqlCommand cmd = new SqlCommand("[api].[LoadBookKeyWords]", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            KeyWord kw;
            Book book;
            using (SqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    if (Lib.IDKeyWords.TryGetValue((int)rd["TAG_ID"], out kw))
                    {
                        if (Lib.IDBooks.TryGetValue((int)rd["BK_ID"], out book))
                        {
                            book.KeyWords.Add(kw);
                            kw.Books.Add(book);
                        }
                    }
                }
            }
        }
        #endregion


        /// <summary>
        /// Loading books, from db and linking Series and Languages
        /// </summary>
        /// <param name="Lib"> Whole Lib </param>
        static void LoadBooksFromDb(Library Lib) {
            SqlCommand cmd = new SqlCommand("[api].[LoadBooks]", Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            Book book;
            Series ser;
            Lang lang;
            using (SqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    if (!Lib.IDSeries.TryGetValue((int)rd["BK_SERIES"], out ser)) ser = null;
                    if (!Lib.IDLangs.TryGetValue((int)rd["BK_LANG"], out lang)) lang = null;
                    book = new Book((int)rd["ID"], (string)rd["BK_NAME"], ser, (int)rd["BK_NO"], (string)rd["BK_FILE_NAME"], (int)rd["BK_SIZE"], (int)rd["BK_DELETED"], (int)rd["BK_FROM_ID"], (string)rd["BK_FILE_EXT"], (DateTime)rd["BK_ADDED"], lang, (int)rd["BK_RATE"]);
                    Lib.IDBooks.Add(book.ID, book);
                    Lib.Books.Add(book);
                }
            }
        }




        #region LoadTags
        /// <summary>
        ///  Зеркалирование для доступа к констуктору
        ///  Отдельный класс для кеширования ссылки на конструктор
        /// </summary>
        /// <typeparam name="T"> Derived from Tag: Lang, Series, Author, KeyWord, Genre </typeparam>
        private static class TagFactory<T>
        {
            public static readonly ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { typeof(int), typeof(string) });
        }

        /// <summary>
        /// Proc for import all Tags into Collection
        /// </summary>
        /// <typeparam name="T"> Derived from Tag: Lang, Series, Author, KeyWord, Genre </typeparam>
        /// <param name="Tags"> Dictionary for Tags Library.IDSeries, Library.IDAuthors...  </param>
        /// <param name="Proc"> MS SQL Stored Procedure Name for getting data in format ID as int, NAME as nVarChar </param>
        public static void LoadTags<T>(Dictionary<int, T> Tags, string Proc) where T:Tag
        {
            SqlCommand cmd = new SqlCommand(Proc, Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            T tag;

            var constructor = TagFactory<T>.constructor;

            using (SqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {

                 tag= constructor.Invoke(new object[] { (int)rd["ID"], (string)rd["NAME"] }) as T;
                 Tags.Add(tag.ID, tag);
                }
            }
        }

        #endregion

    }
}
