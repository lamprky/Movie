using Application.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interations
{
    public static class API
    {
        private static List<Method> actionsWithBody = new List<Method> { Method.POST, Method.PUT };
        private static List<Method> methodWithResponse = new List<Method> { Method.GET, Method.POST };

        public static Dictionary<string, Method> GetMethod = new Dictionary<string, Method>
        {
            { "GET",    Method.GET },
            { "GETID",  Method.GET },
            { "POST",   Method.POST },
            { "PUT",    Method.PUT },
            { "DELETE", Method.DELETE }
        };

        private static Dictionary<string, string> GetController = new Dictionary<string, string>
        {
            { "CT", "ContributorTypes" },
            { "C",  "Contributors" },
            { "G",  "Genres" },
            { "M",  "Movies" }
        };

        public static IRestResponse GetResponse(UserAction action, RestRequest request)
        {
            if (request != null)
            {
                bool hasId = request.Parameters.Where(x => x.Name.ToLower() == "id").Count() > 0;
                var restResp = Call(request, GetController[action.Entity] + (hasId ? "/{id}" : ""));
                IsAnticipatedResponse(restResp);

                if (methodWithResponse.Contains(request.Method))
                    return restResp;
                else
                    Common.ShowSuccesInputMessage("Changes submitted successfully!");
            }
            return null;
        }

        public static RestRequest PrepareRequest(UserAction action)
        {
            RestRequest request = new RestRequest();
            Method method = GetMethod[action.Method];

            if (actionsWithBody.Contains(method))
            {
                object body = Common.GetEntity(method, action.Entity);
                if (body == null)
                    return null;
                request = CreateRequest(method, body);
            }
            else
            {
                object id = null;
                if (action.Method != "GET")
                {
                    var key = Common.GetKey();
                    if (key == null)
                        return null;
                    else
                        id = key;
                }
                request = CreateRequestByID(method, id);
            }
            return request;
        }

        public static IRestResponse Call(RestRequest request, string controller)
        {
            string Url = ConfigurationManager.AppSettings.Get("Url");
            var client = new RestClient(Url + controller);

            var response = client.Execute(request);

            return response;
        }

        public static RestRequest CreateRequest(Method method, object body)
        {
            //post & put
            var request = new RestRequest(method);
            request.AddJsonBody(body);

            return request;
        }

        public static RestRequest CreateRequestByID(Method method, object id)
        {
            //get by id & get all & delete
            var request = new RestRequest(method);

            if (id != null)
                request.AddUrlSegment("id", id);

            return request;
        }

        public static void IsAnticipatedResponse(IRestResponse restResp)
        {
            if (!(restResp.StatusCode == HttpStatusCode.OK
                || restResp.StatusCode == HttpStatusCode.Created
                || restResp.StatusCode == HttpStatusCode.NoContent
                ))
                Console.WriteLine(restResp.StatusDescription);
        }

        public static T DeserializeResponse<T>(IRestResponse response) where T : class
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public static void PreviewRequest(object obj)
        {
            Console.Clear();
            var json = JsonConvert.SerializeObject(obj);

            Console.WriteLine("So, this is our request to the API!");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(json);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }
    }
}