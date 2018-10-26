using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 位置搜索.Models
{
    public class GetLatAndLngByIP
    {
        public int status { get; set; }
        public string message { get; set; }
        public GetLatAndLngByIPResultModel result { get; set; }
    }

    public class GetLatAndLngByIPResultModel
    {
        public Dictionary<string,string> location { get; set; }
    }
}