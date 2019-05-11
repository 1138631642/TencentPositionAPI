using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace 测试
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://apis.map.qq.com/ws/place/v1/search?keyword=%E9%85%92%E5%BA%97&boundary=nearby(116.405285,39.904989,1000)&key=xx";
            HttpClient request = new HttpClient();
            string result = request.GetStringAsync(url).Result;

            var obj = JsonConvert.DeserializeObject(result);
            Console.WriteLine(obj);

            //ResultModel obj = JsonConvert.DeserializeObject<ResultModel>(result);
            //Console.WriteLine(obj.status.ToString());
            //Console.WriteLine(obj.count.ToString());
            //Console.WriteLine(obj.message.ToString());
            //Console.WriteLine(obj.request_id.ToString());
            //Console.WriteLine(obj.data);
            //Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
