using ConsoleAppLibraryV1.Helper;
using ConsoleAppLibraryV1.Models;
using ConsoleAppLibraryV1.StableModels;
using ConsoleAppLibraryV1.Storage;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ConsoleAppLibraryV1
{
    internal class Program
    {
        const string databaseFile = "database.dat";
        
        static GenericStore<Authors> authorsStore = new GenericStore<Authors>();
        static GenericStore<Books> booksStore = new GenericStore<Books>();
        
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            using (FileStream fileStream = File.Open(databaseFile, FileMode.OpenOrCreate))
            {
                BinaryFormatter bf = new BinaryFormatter();
                var db = (Database)bf.Deserialize(fileStream);

                if (db != null)
                {
                    authorsStore = db.Author;
                    booksStore = db.Book;
                }
            }

            int id;
            bool allowForClear = true;
            Authors authors;
            Books books;

            Menu menu;

        l1:
            menu = Extension.PrintMenu();

            switch (menu)
            {
                case Menu.AuthorGetAll:
                    #region For All
                    Console.Clear();
                    if (authorsStore.Length == 0)
                    {
                        Console.WriteLine("Müəlliflər boşdur, yeni müəllif əlavə edin...");
                        goto case Menu.AuthorAdd;
                    }

                    ShowAllAuthors(false);
                    goto l1;
                #endregion
                case Menu.AuthorGetById:
                    ShowAllAuthors(true);
                l2:
                    id = Extension.ReadInteger("Müəllif id: ", true, authorsStore.Min(x => x.Id), authorsStore.Max(x => x.Id));

                    authors = authorsStore.Find(id);

                    if (authors == null)
                    {
                        Console.WriteLine($"Bu müəllif mövcud deyil");
                        goto l1;
                    }
                    Console.WriteLine(authors);
                    goto l1;
                case Menu.AuthorFindByName:
                    Console.Clear();
                    Console.WriteLine("=======Müəlliflər=======");
                    foreach (var item in authorsStore)
                    {
                        Console.WriteLine($"{item.Id} {item.Name} {item.Surname}");
                    }
                    Console.WriteLine("========================");
                l6:
                    string name = Extension.ReadString("Müəllif adı ilə axtar: ");
                    var data = authorsStore.FindName(name);
                    if (data.Length == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Bu müəllif mövcud deyil");
                        Console.ResetColor();
                        goto l6;
                    }
                    foreach (var item in data)
                    {
                        Console.WriteLine(item);
                    }
                    goto l1;
                case Menu.AuthorAdd:
                    if (allowForClear)
                        Console.Clear();
                    authors = new Authors();
                    authors.Name = Extension.ReadString("Müəllif adını yaz: ");
                    authors.Surname = Extension.ReadString("Müəllif soyadını yaz: ");
                    authorsStore.Add(authors);
                    Console.WriteLine("Əlavə edildi");
                    goto l1;

                case Menu.AuthorEdit:

                    ShowAllAuthors(true);
                    id = Extension.ReadInteger("Müəllif id: ", true, authorsStore.Min(x => x.Id), authorsStore.Max(x => x.Id));

                    authors = authorsStore.Find(id);
                    if (authors == null)
                    {
                        goto case Menu.AuthorEdit;
                    }

                    authors.Name = Extension.ReadString("Müəllif adı: ");
                    authors.Surname = Extension.ReadString("Müəllif soyadı: ");
                    goto case Menu.AuthorGetAll;


                case Menu.AuthorRemove:
                    ShowAllAuthors(true);
                    id = Extension.ReadInteger("Müəllif id: ", true, authorsStore.Min(x => x.Id), authorsStore.Max(x => x.Id));

                    authors = authorsStore.Find(id);
                    if (authors == null)
                    {
                        goto case Menu.AuthorRemove;
                    }

                    authorsStore.Remove(authors);

                    goto case Menu.AuthorGetAll;
               
                case Menu.BookGetAll:
                    
                    Console.Clear();
                    if (booksStore.Length == 0)
                    {
                        Console.WriteLine("Kitablar boşdur, yeni kitab əlavə edin...");
                        goto case Menu.BookAdd;
                    }

                    ShowAllBooks(false);
                    goto l1;
                    
                case Menu.BookGetById:
                    ShowAllBooks(true);
                    Console.WriteLine($"=========== ======== ===========");
                l5:
                    id = Extension.ReadInteger("Kitab id: ", true, booksStore.Min(x => x.Id), booksStore.Max(x => x.Id));

                    authors = authorsStore.Find(id);

                    if (authors == null)
                    {
                        Console.WriteLine($"Bu müəllif mövcud deyil");
                        goto l5;
                    }
                    Console.WriteLine(authors);
                    goto l1;
                case Menu.BookFindByName:
                    Console.Clear();
                    Console.WriteLine("======= KİTABLAR =======");
                    foreach (var item in booksStore)
                    {
                        Console.WriteLine($"{item.Id} {item.Name}");
                    }
                    Console.WriteLine("========================");
                l7:
                    string name1 = Extension.ReadString("Kitab adı ilə axtar: ");
                    var data1 = booksStore.FindName(name1);
                    if (data1.Length == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Bu kitab mövcud deyil");
                        Console.ResetColor();
                        goto l7;
                    }
                    foreach (var item in data1)
                    {
                        Console.WriteLine(item);
                    }
                    goto l1;
                case Menu.BookAdd:
                    Console.Clear();
                    if (authorsStore.Length == 0)
                    {
                        allowForClear = false;
                        Console.WriteLine("Müəlliflər boşdur, ilk öncə müəllif əlavə edilməlidir!");
                        goto case Menu.AuthorAdd;
                    }
                    books = new Books();
                l8:
                    ShowAllAuthors(false);
                    id = Extension.ReadInteger("Müəllif id: ", true, authorsStore.Min(x => x.Id), authorsStore.Max(x => x.Id));

                    authors = authorsStore.Find(id);
                    if (authors == null)
                    {
                        goto l8;
                    }

                    books.AuthorsId = authors.Id;
                    books.Name = Extension.ReadString("Kitab adını yaz: ");
                    books.Genre = Extension.ReadEnum<Genre>("Janr: ");
                    books.PageCount = Extension.ReadUInt16("Səhifə sayı: ", true, 50, 1000);
                    books.Price = Extension.ReadDecimal("Qiymət: ", true, 1);

                    booksStore.Add(books);

                    ShowAllBooks(true);
                    goto l1;
                case Menu.BookEdit:
                    ShowAllBooks(true);

                    id = Extension.ReadInteger("Kitab id: ", true, booksStore.Min(x => x.Id), booksStore.Max(x => x.Id));

                    books = booksStore.Find(id);
                    if (books == null)
                    {
                        goto case Menu.AuthorEdit;
                    }

                    books.Name = Extension.ReadString("Kitab adı: ");

                    ShowAllAuthors(false);
                l4:
                    id = Extension.ReadInteger("Müəllif id: ", true, authorsStore.Min(x => x.Id), authorsStore.Max(x => x.Id));

                    if (!authorsStore.Any(x => x.Id == id))
                    {
                        Console.WriteLine($"Müəllif mövcud deyil, siyahıdan seçin!");

                        goto l4;
                    }
                    books.AuthorsId = id;
                    goto case Menu.BookGetAll;
                case Menu.BookRemove:
                    ShowAllBooks(true);
                    id = Extension.ReadInteger("Model id: ", true, booksStore.Min(x => x.Id), booksStore.Max(x => x.Id));

                    books = booksStore.Find(id);
                    if (books == null)
                    {
                        goto case Menu.BookRemove;
                    }

                    booksStore.Remove(books);

                    goto case Menu.BookGetAll;
                case Menu.SaveAndExit:

                    Database db = new Database();
                    db.Author = authorsStore;
                    db.Book = booksStore;

                    FileStream fileStream = File.Create(databaseFile);

                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fileStream, db);
                    fileStream.Flush();
                    fileStream.Close();

                    Console.WriteLine("Çıxış üçün hər hansı düyməni sıxın!");
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }

        private static void ShowAllBooks(bool clearBefore)
        {
            if (clearBefore)
            {
                Console.Clear();
            }

            Console.WriteLine($"=========== KİTABLAR ===========");
            foreach (var item in booksStore)
            {
                var authors = authorsStore.Find(item.AuthorsId);

                Console.WriteLine($"{item.Id}. {authors.Name} {authors.Surname}\n{item}\n-------------------------------------------");
            }
            Console.WriteLine($"=========== ======== ===========");
        }

        private static void ShowAllAuthors(bool clearBefore)
        {
            if (clearBefore)
            {
                Console.Clear();
            }

            Console.WriteLine($"=========== MÜƏLLIFLƏR ===========");
            foreach (var item in authorsStore)
            {
                Console.WriteLine($"{item.Id} {item.Name} {item.Surname}");
            }
            Console.WriteLine($"=========== ======== ===========");
        }
    }
}