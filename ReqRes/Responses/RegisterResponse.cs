using ReqRes.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReqRes.Responses
{
    public class RegisterResponse
    {
        public string accessToken { get; set; }
        public CompleteUserData user { get; set; }
    }
}
