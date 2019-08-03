using Application.Models;
using RestSharp;
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

            while (!(input.ToUpper() == Yes || input.ToUpper() == No))
            {
                ShowWrongInputMessage("Y or N?");
                input = GetUserInput();
            }

            return input.ToUpper();
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

        public static Guid? GetKey()
        {
            Console.WriteLine("To this, we should know the key of the entity.");

            var input = GetYesOrNoAnswer("Do you already have the key?");
            if (input == Yes)
            {
                Console.WriteLine("We are listening!");
                Guid key = Guid.Empty;
                var isValidGuid = false;
                bool exit = false;
                while (!isValidGuid && !exit)
                {
                    input = GetUserInput();
                    if (input.ToUpper() == "R")
                        exit = true;
                    else if (Guid.TryParse(input, out key))
                        isValidGuid = true;
                    else
                        ShowWrongInputMessage("We are waiting for a GUID! If you finally don't know the key, just press R to return!");
                }
                if (exit)
                    return null;

                return key;
            }
            else
            {
                Console.WriteLine("To find your key, you need to make a GET call first! ");
            }
            return null;
        }

        public static object GetEntity(Method method, string entity)
        {
            object obj;

            if (method == Method.POST)
                obj = Common.GetPostEntity[entity]();
            else
                obj = Common.GetPutEntity[entity]();

            return obj;
        }

        public static Dictionary<string, Func<object>> GetPostEntity = new Dictionary<string, Func<object>>
        {
            { "CT", () => {  return ContributorType.CreateContributorType();  } },
            //{ "C", (object) => CreateContributorType(id, translations)},
            //{ "G", (id, translations) => CreateContributorType(id, translations)},
            //{ "M", (id, translations) => CreateContributorType(id, translations)},
        };

        public static Dictionary<string, Func<object>> GetPutEntity = new Dictionary<string, Func<object>>
        {
            { "CT", () => {  return ContributorType.UpdateContributorType();  } },
               //{ "C", (object) => CreateContributorType(id, translations)},
               //{ "G", (id, translations) => CreateContributorType(id, translations)},
               //{ "M", (id, translations) => CreateContributorType(id, translations)},
           };

        public static Dictionary<string, Action<string, IRestResponse>> ShowResult =
                  new Dictionary<string, Action<string, IRestResponse>>
        {
            { "CT", (method, obj) => ContributorType.PreviewResponse(method, ToViewModel<ContributorTypeViewModel>(method, obj))},
               //{ "C", (object) => CreateContributorType(id, translations)},
               //{ "G", (id, translations) => CreateContributorType(id, translations)},
               //{ "M", (id, translations) => CreateContributorType(id, translations)},
        };

        public static List<T> ToViewModel<T>(string method, IRestResponse obj) where T : class
        {
            List<T> model = new List<T>();
            if (method == "GET")
                model = API.DeserializeResponse<List<T>>(obj);
            else
                model.Add(API.DeserializeResponse<T>(obj));
            return model;
        }
    }
}