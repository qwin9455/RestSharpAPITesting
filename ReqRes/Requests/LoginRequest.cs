using ReqRes.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReqRes.Api
{
    public class LoginRequest : IJsonRequest
    {
        public string email { get; set; }
        public string password { get; set; }

        public LoginRequest(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
