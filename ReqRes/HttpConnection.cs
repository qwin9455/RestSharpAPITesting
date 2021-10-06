using Newtonsoft.Json;
using ReqRes.Api;
using ReqRes.Requests;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReqRes
{
    public static class HttpConnection<TResponse> where TResponse : class, new()
    {
        public static string baseUrl = "http://localhost:3000/";

        public static RestClient SetUrl(string endpoint)
        {
            var url = Path.Combine(baseUrl, endpoint);
            return new RestClient(url);
        }

        public static ResponseObject<TResponse> Post(string endpoint, IJsonRequest request)
        {
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddParameter("application/json", request.ToJsonString(), ParameterType.RequestBody);
            var response = SetUrl(endpoint).Execute(restRequest);

            return new ResponseObject<TResponse>(response);
            
        }

        public static ResponseObject<TResponse> Get(string endpoint, string token = "")
        {
            var restRequest = new RestRequest(Method.GET);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Authorization", "Bearer " + token);

            var response = SetUrl(endpoint).Execute(restRequest);

            return new ResponseObject<TResponse>(response);

        }

    }
}
