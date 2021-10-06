using System;
using System.Collections.Generic;
using System.Text;

namespace ReqRes.Requests
{
    public interface IJsonRequest
    {
        string ToJsonString() {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
