using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBLM.Helper
{
    public static class ConsoleHelper
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
        public static void Output(string format, params object[] arg) 
        {
            Console.WriteLine(format, arg);
        }
        public static void OutputLine(string message, bool OutputBlankLineBeforeMessage = true) 
        {
            if (OutputBlankLineBeforeMessage)
            {
                Console.WriteLine();
            }
            Console.WriteLine(message);
        }
        public static void OutputLine(string format, params object[] arg) 
        {
            Console.WriteLine(format, arg);
        }
        public static void OutputBlankLine() 
        {
            Console.WriteLine();
        }
    }
}
