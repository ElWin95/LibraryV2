using LibraryV2.Category;
using LibraryV2.Helpers;
using LibraryV2.Library;
using LibraryV2.Storage;
using LibraryV2.Storage.IIdentity;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace LibraryV2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Unicode
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            #endregion

            #region Store
            GenericStore<Authors> authorStore = new GenericStore<Authors>();
            GenericStore<Books> bookStore = new GenericStore<Books>();
            #endregion

            const string databaseFile = "database.dat";

            using (FileStream fileStream = File.Open(databaseFile, FileMode.OpenOrCreate))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                var db = (Database)binaryFormatter.Deserialize(fileStream);

                if (db != null)
                {
                    authorStore = db.Author;
                    bookStore = db.Book;
                }
            }

            bool allowClear = true;
            int id;
            Authors author;
            Books book;
            Menu menu;
        l1:
            menu = Extension.PrintMenu();

            switch (menu)
            {
                #region AuthorAdd
                case Menu.AuthorAdd:
                    if (allowClear)
                        Console.Clear();
                    Authors aut = new Authors();
                    aut.Name = Extension.ReadString("Müəllifin adını yazın: ");
                    aut.Surname = Extension.ReadString("Müəllifin soyadını yazın: ");
                    authorStore.Add(aut);
                    Console.WriteLine("Əlavə edildi");
                    goto l1;
                #endregion

                #region AuthorGetAll
                case Menu.AuthorGetAll:
                    Console.Clear();

                    if (authorStore.Length == 0)
                    {
                        Console.WriteLine("Müəllif boşdur, yeni müəllif əlavə edin");
                        goto case Menu.AuthorAdd;
                    }
                    Console.WriteLine("=======Müəlliflər=======");
                    foreach (var item in authorStore)
                    {
                        Console.WriteLine($"{item.Id} {item.Name} {item.Surname}");
                    }
                    Console.WriteLine("========================");
                    goto l1;
                #endregion

                #region AuthorFindByName

                case Menu.AuthorFindByName:
                    Console.Clear();
                    Console.WriteLine("=======Müəlliflər=======");
                    foreach (var item in authorStore)
                    {
                        Console.WriteLine($"{item.Id} {item.Name} {item.Surname}");
                    }
                    Console.WriteLine("========================");
                l6:
                    string name = Extension.ReadString("Müəllif adı ilə axtarış: ");
                    var data = authorStore.FindName(name);
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

                #endregion

                #region AuthorGetById

                case Menu.AuthorGetById:
                    Console.Clear();
                    Console.WriteLine("=======Müəlliflər=======");
                    foreach (var item in authorStore)
                    {
                        Console.WriteLine($"{item.Id} {item.Name} {item.Surname}");
                    }
                    Console.WriteLine("========================");
                l2:
                    id = ValueExtension.ReadInt("Müəllif id ilə axtarış: ", true, authorStore.Min(x => x.Id), authorStore.Max(x => x.Id));
                    author = authorStore.Find(id);
                    if (author == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Bu müəllif mövcud deyil");
                        Console.ResetColor();
                        goto l1;
                    }
                    Console.WriteLine(author);
                    goto l1;

                #endregion

                #region AuthorEdit

                case Menu.AuthorEdit:
                    Console.Clear();
                    Console.WriteLine("=======Müəlliflər=======");
                    foreach (var item in authorStore)
                    {
                        Console.WriteLine($"{item.Id} {item.Name} {item.Surname}");
                    }
                    Console.WriteLine("========================");
                    id = ValueExtension.ReadInt("Müəllif id: ", true, authorStore.Min(x => x.Id), authorStore.Max(x => x.Id));
                    author = authorStore.Find(id);
                    if (author == null)
                    {
                        goto case Menu.AuthorEdit;
                    }
                    author.Name = Extension.ReadString("Müəllif adı: ");
                    author.Surname = Extension.ReadString("Müəllif Soyadı: ");
                    goto case Menu.AuthorGetAll;

                #endregion

                #region AuthorRemove

                case Menu.AuthorRemove:

                    Console.Clear();
                    Console.WriteLine("=======Müəlliflər=======");
                    foreach (var item in authorStore)
                    {
                        Console.WriteLine($"{item.Id} {item.Name} {item.Surname}");
                    }
                    Console.WriteLine("========================");
                    id = ValueExtension.ReadInt("Müəllif id: ", true, authorStore.Min(x => x.Id), authorStore.Max(x => x.Id));
                    author = authorStore.Find(id);
                    if (author == null)
                    {
                        goto case Menu.AuthorRemove;
                    }
                    authorStore.Remove(author);
                    goto case Menu.AuthorGetAll;

                #endregion

                #region BookAdd

                case Menu.BookAdd:
                    Console.Clear();
                    if (authorStore.Length == 0)
                    {
                        allowClear = false;
                        Console.WriteLine("Müəlliflər boşdur, müəllif əlavə edin");
                        goto case Menu.AuthorAdd;
                    }
                    book = new Books();
                    book.Name = Extension.ReadString("Kitabın adını yazın: ");
                    book.PageCount = ValueExtension.ReadIntager("Kitabın səhifə sayını daxil edin: ");
                    book.Genre = Extension.ReadGenre("Kitabın janrını daxil edin: ");
                    book.Price = ValueExtension.ReadDecimal("Kitabın qiymətini daxil edin: ");
                    Console.WriteLine("=======Müəlliflər=======");
                    foreach (var item in authorStore)
                    {
                        Console.WriteLine($"{item.Id} {item.Name} {item.Surname}");
                    }
                    Console.WriteLine("========================");
                l3:
                    id = ValueExtension.ReadInt("Müəllif id: ", true, authorStore.Min(x => x.Id), authorStore.Max(x => x.Id));
                    if (!authorStore.Any(x => x.Id == id))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Müəllif mövcud deyil,siyahıdan seçin");
                        Console.ResetColor();
                        goto l3;
                    }
                    book.AuthorsId = id;
                    bookStore.Add(book);
                    Console.WriteLine("Əlavə edildi");
                    goto l1;

                #endregion

                #region BookGetAll
                case Menu.BookGetAll:

                    Console.Clear();
                    if (bookStore.Length == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Kitablar boşdur, yeni kitab əlavə edin");
                        Console.ResetColor();
                        goto case Menu.BookAdd;
                    }
                    Console.WriteLine("=======Kitablar=======");
                    foreach (var item in bookStore)
                    {
                        author = authorStore.Find(item.AuthorsId);
                        Console.WriteLine($"{item.Id}\nKitabın adı: {item.Name}\nMüəllif: {author.Name} {author.Surname}\nSəhifə sayı: {item.PageCount}" +
                            $"\nJanrı: {item.Genre}\nQiyməti: {item.Price} ₼");
                    }
                    Console.WriteLine("========================");
                    goto l1;
                #endregion

                #region BookFindByName

                case Menu.BookFindByName:
                    Console.Clear();
                    Console.WriteLine("=======Kitablar=======");
                    foreach (var item in bookStore)
                    {
                        Console.WriteLine($"{item.Id} {item.Name}");
                    }
                    Console.WriteLine("========================");
                l7:
                    string name1 = Extension.ReadString("Kitab adı ilə axtarış: ");
                    var data1 = bookStore.FindName(name1);
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

                #endregion

                #region BokkGetById

                case Menu.BookGetById:
                    Console.Clear();
                    Console.WriteLine("=======Kitablar=======");
                    foreach (var item in bookStore)
                    {
                        author = authorStore.Find(item.AuthorsId);
                        Console.WriteLine($"{item.Id} {item.Name} | {author.Name} {author.Surname}");
                    }
                    Console.WriteLine("========================");
                l5:
                    id = ValueExtension.ReadInt("Kitab id: ", true, bookStore.Min(x => x.Id), bookStore.Max(x => x.Id));
                    author = authorStore.Find(id);
                    if (author == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Bu kitab mövcud deyil");
                        Console.ResetColor();
                        goto l5;
                    }
                    Console.WriteLine(author);
                    goto l1;

                #endregion

                #region BookEdit

                case Menu.BookEdit:

                    Console.Clear();
                    Console.WriteLine("=======Kitablar=======");
                    foreach (var item in bookStore)
                    {
                        author = authorStore.Find(item.AuthorsId);
                        Console.WriteLine($"{item.Id} {item.Name} | {author.Name} {author.Surname}");
                    }
                    Console.WriteLine("========================");
                    id = ValueExtension.ReadInt("Kitab id: ", true, bookStore.Min(x => x.Id), bookStore.Max(x => x.Id));
                    book = bookStore.Find(id);
                    if (book == null)
                    {
                        goto case Menu.AuthorEdit;
                    }
                    book.Name = Extension.ReadString("Kitabın adı: ");
                    Console.WriteLine("=======Müəlliflər=======");
                    foreach (var item in authorStore)
                    {
                        Console.WriteLine($"{item.Id} {item.Name} {item.Surname}");
                    }
                    Console.WriteLine("========================");
                l4:
                    id = ValueExtension.ReadInt("Müəllif id: ", true, authorStore.Min(x => x.Id), authorStore.Max(x => x.Id));
                    if (!authorStore.Any(x => x.Id == id))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Müəllif mövcud deyil,siyahıdan seçin");
                        Console.ResetColor();
                        goto l4;
                    }
                    book.AuthorsId = id;
                    goto case Menu.BookGetAll;

                #endregion

                #region BookRemove
                case Menu.BookRemove:

                    Console.Clear();
                    Console.WriteLine("=======Müəlliflər=======");
                    foreach (var item in bookStore)
                    {
                        author = authorStore.Find(item.AuthorsId);
                        Console.WriteLine($"{item.Id} {item.Name} | {author.Name} {author.Surname}");
                    }
                    Console.WriteLine("========================");
                    id = ValueExtension.ReadInt("Kitab id: ", true, bookStore.Min(x => x.Id), bookStore.Max(x => x.Id));
                    book = bookStore.Find(id);
                    if (book == null)
                    {
                        goto case Menu.BookRemove;
                    }
                    bookStore.Remove(book);
                    goto l1;

                #endregion

                #region SAVEandEXIT
                case Menu.SAVEandEXIT:
                    Database datbas = new();
                    datbas.Author = authorStore;
                    datbas.Book = bookStore;

                    FileStream fileStream = File.Create(databaseFile);
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, datbas);
                    fileStream.Flush();
                    fileStream.Close();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Çıxış üçün hər-hansı düyməni sıxın!");
                    Console.ResetColor();
                    Console.ReadKey();
                    break;
                #endregion

                default:
                    break;
            }


        }

    }
}