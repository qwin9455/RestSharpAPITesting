using System;
using System.Collections.Generic;
using System.Text;

namespace ReqRes.Responses
{
    public class ReadUserResponse
    {
        public string email { get; set; }
        public string password { get; set; }
        public long id { get; set; }
    }
}
