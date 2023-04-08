using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV2.Helpers
{
    public class ValueExtension
    {
        public static int ReadInt(string question, bool check = false, int minValue = 0, int maxValue = 0)
        {
            int value;
        l1:
            Console.Write(question);
            if (!int.TryParse(Console.ReadLine(), out value))
            {
                goto l1;
            }
            if (check && (value < minValue || value > maxValue))
            {
                Console.WriteLine($"{value} bu intervalda deyil [{minValue}, {minValue}]");
            }
            return value;
        }
        public static int ReadIntager(string question)
        {
            int value;
        l1:
            Console.Write(question);
            if (!int.TryParse(Console.ReadLine(), out value) || value <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Uyğun ədəd daxil edin");
                Console.ResetColor();
                goto l1;
            }
            return value;
        }
        public static decimal ReadDecimal(string question)
        {
            decimal value;
        l1:
            Console.Write(question);
            if (!decimal.TryParse(Console.ReadLine(), out value) || value <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Uyğun ədəd daxil edin");
                Console.ResetColor();
                goto l1;
            }
            return value;
        }

    }
}
