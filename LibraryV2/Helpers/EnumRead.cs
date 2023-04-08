using LibraryV2.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV2.Helpers
{
    public static class Extension
    {
        public static Menu PrintMenu()
        {
            ConsoleColor reng = Console.ForegroundColor;
            Type type = typeof(Menu);
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("=======LIBRARY=======");
            foreach (var item in Enum.GetValues(type))
            {
                Console.WriteLine($"{((int)item).ToString().PadLeft(2, '0')}. {item}");
            }
            Console.WriteLine("======================");
            Console.ForegroundColor = reng;

        l1:
            Console.Write("Rejimi seçin: ");
            if (!Enum.TryParse<Menu>(Console.ReadLine(), out Menu selectedMenu)
                || !Enum.IsDefined(type, selectedMenu))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Düzgün daxil edilməyib");
                Console.ResetColor();
                goto l1;
            }
            return selectedMenu;

        }

        public static Genre ReadGenre(string question)
        {
            Console.Clear();

            Console.WriteLine(question);
            Type type = typeof(Genre);
            Console.Clear();
            Console.WriteLine("Janr seçin");
            foreach (var item in Enum.GetValues(type))
            {
                Console.WriteLine($"{((byte)item).ToString().PadLeft(2, '0')}. {item}");
            }
            Console.WriteLine("======================");
        l1:
            Console.Write("Rejimi seçin: ");
            if (!Enum.TryParse<Genre>(Console.ReadLine(), out Genre selectedGenre)
                || !Enum.IsDefined(type, selectedGenre))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Düzgün daxil edilməyib");
                Console.ResetColor();
                goto l1;
            }
            return selectedGenre;

        }

        public static string ReadString(string question)
        {
        l1:
            Console.Write(question);
            string value = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(value) || value.Any(Char.IsDigit))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Mətn daxil edin");
                Console.ResetColor();
                goto l1;
            }
            return value.ChangeString();
        }

        public static string ChangeString(this string word)
        {
            StringBuilder build = new StringBuilder();
            string newWord = word.ToLower();
            build.Append(char.ToUpper(newWord[0]));
            build.Append(newWord.Substring(1));
            return build.ToString();
        }

    }
}
