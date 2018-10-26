using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 位置搜索.Models
{
    public class GetLatAndLngModel
    {
        public int status { get; set; }
        public string message { get; set; }
        public GetLatAndLngResultModel result { get; set; }
        
    }

    public class GetLatAndLngResultModel
    {
        //public string title { get; set; }
        public Dictionary<string,string> location { get; set; }
    }
}