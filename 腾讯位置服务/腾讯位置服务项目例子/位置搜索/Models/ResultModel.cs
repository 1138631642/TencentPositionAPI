using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 位置搜索.Models
{
    public class ResultModel
    {
        public int status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public string request_id { get; set; }
        public List<Data> data { get; set; }
    }
}