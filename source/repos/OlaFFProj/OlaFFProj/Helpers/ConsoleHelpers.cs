using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlaFFProj.Helpers
{
    static class ConsoleHelpers
    {
        //Reads user input from the console "prompt" user prompt.
        //"forceToLowercase" Whether or not to force the user's provided input to lowercase text.
        //returns > A user's provided input as a string.
        public static string ReadInput(string prompt, bool forceToLowerCase = false)
        {
            Console.WriteLine();
            Console.Write(prompt);
            string input = Console.ReadLine();
            return forceToLowerCase ? input.ToLower() : input;
        }
        // Clear the console
        public static void ClearOutput()
        {
            Console.Clear();
        }
        // Writes the provided message to the console. 
        //"message"- message to write to the console
        public static void Output(string message)
        {
            Console.Write(message);
        }
        // Writes the provided format string and args to the console.
        //"format"-string to write to the console. "arg"-arguments to use with the format string.
        public static void Output(string format, params object[] arg)
        {
            Console.Write(format, arg);
        }
        // Writes the provided message to the console as a line.
        //"message"-The message to write to the console
        //"outputBlankLineBeforeMessage"- Whether or not to write a blank line before the message
        public static void OutputLine(string message, bool outputBlankLineBeforeMessage = true) 
        {
            if (outputBlankLineBeforeMessage)
            {
                Console.WriteLine();
            }
            Console.WriteLine(message);
        }
        // Writes the provided format string and args to the console as a line.
        //"format"-The format string to write to the console.
        //"arg"-The arguments to use with the format string.
        public static void OutputLine(string format, params object[] arg) 
        {
            Console.WriteLine(format, arg);
        }
        // Writes a blank line to the console.
        public static void OutputBlankLine() 
        {
            Console.WriteLine();
        }
    }
}
