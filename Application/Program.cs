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
    class Program
    {
        private static string terminateKeyword = "EXIT";
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            var actionsWithBody = new List<string> { "POST", "PUT" };
            var actionsWithId = new List<string> { "DELETE", "GETID", "GET" };
            var methodWithResponse = new List<string> { "POST", "GETID", "GET" };

            MakeIntroduction();

            var read = Common.GetUserInput();
            while (read != terminateKeyword)
            {
                var commands = GetCommand(read);
                var entity = commands[0];
                var action = commands[1];

                RestRequest request = new RestRequest();
                var method = GetMethod[action];

                if (actionsWithBody.Contains(action))
                {
                    object body = GetEntity(method, entity);
                    request = APICall.CreateRequest(method, body);
                }
                else
                {
                    string id = null;
                    request = APICall.CreateRequest(method, id);
                }

                var restResp = APICall.CallAPI(request, GetController[entity]);

                APICall.IsAnticipatedResponse(restResp);

                if (methodWithResponse.Contains(action))
                    ShowResult[entity](method, restResp);
                else
                    Console.WriteLine("Changes uploaded successfully!");


                Console.WriteLine(@"Let's execute a new action now! Remember: You can use keyword " + terminateKeyword + " to complete the demostration!");
                read = Common.GetUserInput();
            }
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
                if (commands.Count() == 2 && GetMethod.ContainsKey(commands[1]) && GetPostEntity.ContainsKey(commands[0]))
                    isValid = true;
                else
                {
                    Common.ShowWrongInputMessage("");
                    input = Common.GetUserInput();
                }
            }
            return commands;
        }

        private static Dictionary<string, Func<object>> GetPostEntity = new Dictionary<string, Func<object>>
        {
            { "CT", () => {  return ContributorType.CreateContributorType();  } },
            //{ "C", (object) => CreateContributorType(id, translations)},
            //{ "G", (id, translations) => CreateContributorType(id, translations)},
            //{ "M", (id, translations) => CreateContributorType(id, translations)},
        };

        private static Dictionary<string, Action<object>> GetPutEntity = new Dictionary<string, Action<object>>
        {
            { "CT", (obj) => ContributorType.CreateContributorType()},
               //{ "C", (object) => CreateContributorType(id, translations)},
               //{ "G", (id, translations) => CreateContributorType(id, translations)},
               //{ "M", (id, translations) => CreateContributorType(id, translations)},
           };

        private static Dictionary<string, Action<Method, IRestResponse>> ShowResult = new Dictionary<string, Action<Method, IRestResponse>>
        {
            { "CT", (method, restResp) => ContributorType.PreviewResponse(method, restResp)},
               //{ "C", (object) => CreateContributorType(id, translations)},
               //{ "G", (id, translations) => CreateContributorType(id, translations)},
               //{ "M", (id, translations) => CreateContributorType(id, translations)},
           };

        private static Dictionary<string, string> GetController = new Dictionary<string, string>
        {
            { "CT", "ContributorTypes" },
            { "C", "Contributors" },
            { "G", "Genres" },
            { "M", "Movies" }
        };

        private static Dictionary<string, Method> GetMethod = new Dictionary<string, Method>
        {
            { "GET", Method.GET },
            { "GETID", Method.GET },
            { "POST", Method.POST },
            { "PUT", Method.PUT },
            { "DELETE", Method.DELETE }
        };

        private static object GetEntity(Method method, string entity)
        {
            object obj;

            if (method == Method.POST)
                obj = GetPostEntity[entity]();
            else
                obj = GetPutEntity[entity];

            return obj;
        }
    }
}
