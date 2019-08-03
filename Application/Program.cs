using Application.Interations;
using Application.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    internal class Program
    {
        private static string terminateKeyword = "EXIT";

        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            MakeIntroduction();

            var read = Common.GetUserInput();
            while (read.ToUpper() != terminateKeyword)
            {
                var commands = GetCommand(read);
                UserAction action = new UserAction(commands[1], commands[0]);

                RestRequest request = API.PrepareRequest(action);
                if (request != null)
                {
                    //if (action.Method == "POST" || action.Method == "PUT")
                    //    API.PreviewRequest(request);
                    IRestResponse resp = API.GetResponse(action, request);
                    if (resp != null)
                        Common.ShowResult[action.Entity](action.Method, resp);
                }

                Console.WriteLine("\nLet's execute a new action now! Remember: You can use keyword " + terminateKeyword + " to complete the demostration!");
                read = Common.GetUserInput();
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void MakeIntroduction()
        {
            Console.WriteLine("Hello! \n");
            Console.WriteLine("You can use your keyboard to make some dummy test on the API!");
            Console.WriteLine("Combine entity with verb to execute an action! \n");

            Console.WriteLine("Available entities :");
            Console.WriteLine(" - For Contributor Type, use     CT");
            Console.WriteLine(" - For Contributor, use          C");
            Console.WriteLine(" - For Genre, use                G");
            Console.WriteLine(" - For Movie, use                M \n");

            Console.WriteLine("Available actions :");
            Console.WriteLine(" - For GET, use                      GET");
            Console.WriteLine(" - For GET with specific ID, use     GETID");
            Console.WriteLine(" - For POST, use                     POST");
            Console.WriteLine(" - For PUT, use                      PUT");
            Console.WriteLine(" - For DELETE, use                   DELETE \n");

            Console.WriteLine("For example you can use \"CT POST\" to create a new (dummy) contributor type");
            Console.WriteLine("You can use keyword " + terminateKeyword + " to complete the demostration");
        }

        private static string[] GetCommand(string input)
        {
            bool isValid = false;
            var commands = new string[] { };
            while (!isValid)
            {
                commands = input.ToUpper().Split(" ");
                if (commands.Count() == 2
                    && API.GetMethod.ContainsKey(commands[1])
                    && Common.GetPostEntity.ContainsKey(commands[0]))
                    isValid = true;
                else
                {
                    Common.ShowWrongInputMessage("");
                    input = Common.GetUserInput();
                }
            }
            return commands;
        }
    }
}