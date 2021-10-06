using System;
using System.Collections.Generic;
using System.Text;

namespace ReqRes.Model
{
    public partial class BasicUserData
    {
        public long id { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }

    public partial class CompleteUserData : BasicUserData
    {
        public long age { get; set; }
    }
}
