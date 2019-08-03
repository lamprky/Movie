using Application.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interations
{
    public static class ContributorType
    {
        public static ContributorTypeViewModel CreateContributorType()
        {
            Console.Clear();
            ContributorTypeViewModel model = new ContributorTypeViewModel();

            Console.WriteLine("All we need for creating a contributor type is to give us some details! In some languages!");

            var input = Common.GetYesOrNoAnswer("Are you ready?");
            if (input == Common.Yes)
            {
                Translation.CreateOrUpdateTranslations(model);
                PreviewRequest(model);
            }
            else
                return null;
            return model;
        }

        public static ContributorTypeViewModel UpdateContributorType()
        {
            Console.Clear();
            ContributorTypeViewModel model = new ContributorTypeViewModel();

            Console.WriteLine("Ok, let's update a contributor type! But to update, we firstly should know which contributor type.");

            var entity = "CT";
            var method = "GETID";
            UserAction action = new UserAction(method, entity);

            RestRequest request = API.PrepareRequest(action);
            if (request != null)
            {
                Console.WriteLine("Take a breathe till we get the details!");
                var resp = API.GetResponse(action, request);
                List<ContributorTypeViewModel> contributorTypes = Common.ToViewModel<ContributorTypeViewModel>(method, resp);

                model = contributorTypes[0];
                string input = Common.GetYesOrNoAnswer("Okey, we are ready! Do you like to modify or add any translations?");
                if (input == Common.Yes)
                {
                    Translation.CreateOrUpdateTranslations(model);
                    PreviewRequest(model);
                }
                else
                    return null;
            }
            else
                return null;
            return model;
        }

        private static void PreviewRequest(object obj)
        {
            Console.Clear();
            var json = JsonConvert.SerializeObject(obj);

            Console.WriteLine("So, this is our request to the API!");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(json);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }

        public static void PreviewResponse(string method, List<ContributorTypeViewModel> contributorTypes)
        {
            Console.WriteLine("\nLet's see how api responsed back!");

            int i = 1;
            foreach (var ct in contributorTypes)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Contributor: ");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(String.Format("{0,-10} | {1,-10}", i + ". ", ct.ID));
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Translation.PreviewTranslations(ct);
                Console.WriteLine("");
                i++;
            }
        }
    }
}