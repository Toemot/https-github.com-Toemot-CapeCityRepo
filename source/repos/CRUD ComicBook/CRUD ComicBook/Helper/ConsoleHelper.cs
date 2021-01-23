using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_ComicBook.Helper
{
    public static class ConsoleHelper
    {
        public static string ReadInput(string prompt, bool forceToLower = false) 
        {
            Console.WriteLine();
            Console.WriteLine(prompt);
            string input = Console.ReadLine();

            return forceToLower ? input.ToLower() : input;
        }

        public static void ClearOutput() 
        {
            Console.Clear();
        }

        public static void Output(string message) 
        {
            Console.WriteLine(message);
        }

        public static void Output(string format, params object[] args) 
        {
            Console.WriteLine(format, args);
        }

        public static void OutputLine(string message, bool outputBlankLineBeforeMessage = true) 
        {
            if (message == null)
                Console.WriteLine();

            Console.WriteLine(message);
        }

        public static void OutputLine(string format, params object[] args) 
        {
            Console.WriteLine(format, args);
        }

        public static void OutputBlankLine() 
        {
            Console.WriteLine();
        }
    }
}
