using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 测试
{
    public class ResultModel
    {
        public int status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public string request_id { get; set; }
        public List<object> data { get; set; }
    }
}
