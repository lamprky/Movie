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
            ContributorTypeViewModel model = new ContributorTypeViewModel();

            Console.WriteLine("All we need for creating a contributor type is to give us some details! In some languages!");

            var input = Common.GetYesOrNoAnswer("Are you ready?");
            if (input == Common.Yes)
                Translation.CreateOrUpdateTranslations(model);

            PreviewRequest(model);
            return model;
        }

        private static void PreviewRequest(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            Console.WriteLine("\nSo, this is our request to the API!");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(json);
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public static void PreviewResponse(Method method, IRestResponse obj)
        {
            List<ContributorTypeViewModel> contributorTypes = new List<ContributorTypeViewModel>();
            Type T = DeserializeTo[method]();
            if (T == typeof(List<ContributorTypeViewModel>))
                contributorTypes = APICall.DeserializeResponse<List<ContributorTypeViewModel>>(obj);
            else
                contributorTypes.Add(APICall.DeserializeResponse<ContributorTypeViewModel>(obj));

            int i = 1;
            foreach (var ct in contributorTypes)
            {
                Console.WriteLine(String.Format("{0,-10} | {1,-10}", i + ". ", ct.ID));
                Translation.PreviewTranslations(ct);
                i++;
            }

            
        }

        private static Dictionary<Method, Func<Type>> DeserializeTo = new Dictionary<Method, Func<Type>>
        {
            { Method.GET, () => {  return typeof(List<ContributorTypeViewModel>);  } },
            { Method.GET, () => {  return typeof(ContributorTypeViewModel);  } },
            { Method.POST,() => {  return typeof(ContributorTypeViewModel);  } },
        };
    }
}
