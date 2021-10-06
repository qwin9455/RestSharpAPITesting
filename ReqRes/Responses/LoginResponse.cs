using ReqRes.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReqRes.Responses
{
    public class LoginResponse
    {
        public string accessToken { get; set; }
        public BasicUserData user { get; set; }
    }
}
