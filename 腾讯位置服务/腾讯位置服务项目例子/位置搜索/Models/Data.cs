using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 位置搜索.Models
{
    public class Data
    {
        public string id { get; set; }
        public string title { get; set; }
        public string address { get; set; }
        public Dictionary<string,string> location { get; set; }
    }
}