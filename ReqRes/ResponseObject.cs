using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ReqRes
{
    public class ResponseObject<TResponse> where TResponse : class, new()
    {
        public HttpStatusCode HttpCode;
        public string ErrorMessage;
        public string Content;

        public ResponseObject(IRestResponse response)
        {
            this.HttpCode = response.StatusCode;
            this.ErrorMessage = response.ErrorMessage;
            this.Content = response.Content;
        }

        public TResponse GetObject
        {
            get
            {
                try
                {
                    return JsonConvert.DeserializeObject<TResponse>(Content);
                }
                catch
                {
                    return new TResponse();
                }

            }
        }

    }
}
