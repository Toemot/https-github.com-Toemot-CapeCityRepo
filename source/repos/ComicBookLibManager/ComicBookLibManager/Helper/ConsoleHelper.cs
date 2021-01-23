using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookLibManager.Helper
{
    public class ConsoleHelper
    {
        public static string ReadInput(string prompt, bool forceToLowercase = false) 
        {
            Console.WriteLine();
            Console.WriteLine(prompt);
            string input = Console.ReadLine();

            return forceToLowercase ? input.ToLower() : input;
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
            if (outputBlankLineBeforeMessage)
            {
                Console.WriteLine();
            }
            Console.WriteLine(message);
        }
        
        public static void OutputLine(string param, params object[] args)
        {
            Console.WriteLine(param, args);
        }

        public static void OutputBlankLine() 
        {
            Console.WriteLine();
        }
    }
}
