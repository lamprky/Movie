using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interations
{
    public static class Common
    {
        public static string Yes = "Y";
        public static string No = "N";

        public static string GetUserInput()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            var input = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            return input;
        }

        public static string GetYesOrNoAnswer(string question)
        {
            Console.WriteLine(question + " " + "Your have to choose between Y and N!");

            var input = GetUserInput();

            while (input.ToUpper() != Yes && input.ToUpper() != No)
            {
                ShowWrongInputMessage("Y or N?");
                input = GetUserInput();
            }

            return input;

        }

        public static void ShowWrongInputMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("We are sorry but your input is not valid :( Let's try one more time!" + " " + message);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }

        public static void ShowSuccesInputMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }

    }
}
