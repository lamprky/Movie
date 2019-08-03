using Application.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Interations
{
    public static class Common
    {
        public static string Yes = "Y";
        public static string No = "N";
        public static string Return = "R";

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
            Console.WriteLine("To do this, we should know the key of the entity.");

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
                    if (input.ToUpper() == Return)
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
            { "CT", () => {  return ContributorType.Create();  } },
            { "C", () => {  return Contributor.Create();  } },
            //{ "G", (id, translations) => CreateContributorType(id, translations)},
            //{ "M", (id, translations) => CreateContributorType(id, translations)},
        };

        public static Dictionary<string, Func<object>> GetPutEntity = new Dictionary<string, Func<object>>
        {
            { "CT", () => {  return ContributorType.Update();  } },
            { "C",  () => {  return Contributor.Update();  } },
               //{ "G", (id, translations) => CreateContributorType(id, translations)},
               //{ "M", (id, translations) => CreateContributorType(id, translations)},
           };

        public static Dictionary<string, Action<string, IRestResponse>> ShowResult =
                  new Dictionary<string, Action<string, IRestResponse>>
        {
            { "CT", (method, obj) => ContributorType.Preview(ToViewModel<ContributorTypeViewModel>(method, obj))},
            { "C",  (method, obj) => Contributor.Preview(ToViewModel<ContributorViewModel>(method, obj))},
               //{ "G", (id, translations) => CreateContributorType(id, translations)},
               //{ "M", (id, translations) => CreateContributorType(id, translations)},
        };

        public static T GetRecordForUpdate<T>(string entity) where T : class
        {
            Console.WriteLine("Firstly, we should know which record want to update.");

            var method = "GETID";
            UserAction action = new UserAction(method, entity);

            RestRequest request = API.PrepareRequest(action);
            if (request == null)
                return null;

            Console.WriteLine("Take a breathe till we get the details!");
            var resp = API.GetResponse(action, request);

            List<T> results = ToViewModel<T>(method, resp);
            T model = results[0];

            return model;
        }

        public static List<T> ToViewModel<T>(string method, IRestResponse obj) where T : class
        {
            List<T> model = new List<T>();
            if (method == "GET")
                model = API.DeserializeResponse<List<T>>(obj);
            else
                model.Add(API.DeserializeResponse<T>(obj));
            return model;
        }

        public static void Preview(List<Guid> guids, string entity)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(entity + ": ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            int i = 1;
            guids.ForEach(x =>
            {
                Console.Write("\t" + i + ". " + x);
                i++;
            });
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }

        public static void HandleRelationShips(List<Guid> relationships, string entity)
        {
            string input = Common.GetYesOrNoAnswer("Do you want to add some new relations?");
            if (input == Common.Yes)
                Common.AddRelationShips(relationships);

            input = Common.GetYesOrNoAnswer("Do you want to remove some relations?");
            if (input == Common.Yes)
                Common.DeleteRelationShips(relationships, entity);
        }

        public static int? GetRelationShipToDelete(List<Guid> relationships, string entity)
        {
            string input = "";
            bool isValid = false;
            bool exit = false;

            int position = 0;

            while (!isValid && !exit)
            {
                Console.WriteLine("Peak a number from the list bellow!");
                Common.Preview(relationships, entity);
                Console.WriteLine("");

                input = GetUserInput();
                if (input.ToUpper() == Return)
                    exit = true;
                else if (!int.TryParse(input, out position))
                    ShowWrongInputMessage("We are waiting for a number! If you finally decide to abort this mission, just press R to return!");
                else
                    isValid = true;
            }

            if (exit)
                return null;

            return position;
        }

        public static void DeleteRelationShips(List<Guid> relationships, string entity)
        {
            string input = "";

            do
            {
                var position = GetRelationShipToDelete(relationships, entity);
                if (position != null)
                {
                    relationships.RemoveAt(position.Value - 1);
                }
                if (relationships.Count() > 0)
                    input = GetYesOrNoAnswer("Do you want to remove another relation?");
                else
                    input = No;
            } while (input == Yes);
        }

        public static void AddRelationShips(List<Guid> relationships)
        {
            string input = "";

            do
            {
                var key = GetKey();
                if (key != null)
                    relationships.Add(key.Value);

                input = Common.GetYesOrNoAnswer("Do you want to add another relation?");
            } while (input == Common.Yes);
        }
    }
}