using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interations
{
    public static class APICall
    {
        public static IRestResponse CallAPI(RestRequest request, string controller)
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

        public static RestRequest CreateRequest(Method method, string id)
        {
            //get by id & get all & delete
            var request = new RestRequest(method);

            if (string.IsNullOrEmpty(id))
                request.Resource = id;

            return request;
        }

        public static void IsAnticipatedResponse(IRestResponse restResp)
        {
            if (restResp.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Something gone wrong. We are very sorry :(");
        }

        public static T DeserializeResponse<T>(IRestResponse response) where T : class
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
