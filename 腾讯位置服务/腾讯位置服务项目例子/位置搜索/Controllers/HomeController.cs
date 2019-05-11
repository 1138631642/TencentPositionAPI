using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using 位置搜索.Models;

namespace 位置搜索.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        #region 获取位置信息
        /// <summary>
        /// 获取位置信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetData()
        {
            string url = "https://apis.map.qq.com/ws/place/v1/search?keyword=旅馆&boundary=nearby(115.992811,29.712034,1000)&page_size=20&page_index=2&key=xxx";
            HttpClient request = new HttpClient();
            string result = request.GetStringAsync(url).Result;

            var obj = JsonConvert.DeserializeObject<ResultModel>(result);
            return Json(obj, JsonRequestBehavior.AllowGet);
        } 
        #endregion

        #region 分页获取位置数据

        /// <summary>
        /// 分页获取位置数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDataByPage(string kw, string address, int pageIndex = 1)
        {

            int pageCount = 10;
            string url = "https://apis.map.qq.com/ws/place/v1/search?key=xx&";
            if (!string.IsNullOrEmpty(kw))
            {
                url += "keyword=" + kw;
            }
            else
            {
                url += "keyword=酒店";
            }

            // 这是根据用户输入的地点进行搜索，如果想通过定位来搜索周边位置，
            // 可以使用boundary =nearby(39.908491,116.374328,1000) 来进行搜索。
            if (!string.IsNullOrEmpty(address))
            {
                url += "&boundary=region(" + address + ",0)"; //搜那块位置的
            }
            else
            {
                url += "&boundary=region(上海,0)"; //搜那块位置的
            }



            url += "&page_size=" + pageCount; // 每页数量

            url += "&page_index=" + pageIndex; // 获取第几页数据


            HttpClient request = new HttpClient();
            string result = request.GetStringAsync(url).Result;

            var obj = JsonConvert.DeserializeObject<ResultModel>(result);
            return Json(obj, JsonRequestBehavior.AllowGet);
        } 
        #endregion


        #region 根据用户筛选的条件获取数据，然后在用户输入字的时候提示

        /// <summary>
        /// 根据用户筛选的条件获取数据，然后在用户输入字的时候提示
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTipData(string kw)
        {


            string url = "https://apis.map.qq.com/ws/place/v1/suggestion/?region=北京&key=xxx&keyword=";

            if (!string.IsNullOrEmpty(kw))
            {
                url += kw;
            }
            else
            {
                url += "美食";
            }

            HttpClient request = new HttpClient();
            string result = request.GetStringAsync(url).Result;

            var obj = JsonConvert.DeserializeObject<ResultModel>(result);
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        #endregion


        // 根据位置的经纬度来获取数据
        public ActionResult GetDataByLocation(string lat,string lng)
        {

            string url = "https://apis.map.qq.com/ws/geocoder/v1/?key=xxx&get_poi=1&location=";

            if(!string.IsNullOrEmpty(lat)&&!string.IsNullOrEmpty(lng))
            {
                url += lat+","+lng;
            }
            else
            {
                url += "39.984154,116.307490"; // 如果用户没有输入，则给个默认值
            }

            HttpClient request = new HttpClient();
            string result = request.GetStringAsync(url).Result;

            //var obj = JsonConvert.DeserializeObject<ResultModel>(result);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // 根据地址获取该位置信息
        public ActionResult GetLocationByAddress(string address)
        {
            string url = "https://apis.map.qq.com/ws/geocoder/v1/?key=xxx&address=";

            if (!string.IsNullOrEmpty(address))
            {
                url += address;              
            }
            else
            {
                url += "北京市海淀区彩和坊路海淀西大街74号";
            }

            HttpClient request = new HttpClient();
            string result = request.GetStringAsync(url).Result;
            // 返回字符串，由前端转换为json格式
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // 根据用户输入的起始位置和终止位置获取导航路线
        public ActionResult GetNavationByAddress(string beginAddr,string endAddr)
        {
            Dictionary<string,string> beginDic = GetLatAndLngByAddrress(beginAddr);
            Dictionary<string,string> endDic = GetLatAndLngByAddrress(endAddr);

            // 39.984042,116.307535&to=39.976249,116.316569&key=YourKey
            string url = "https://apis.map.qq.com/ws/direction/v1/walking/?key=xx&from=";

            url += beginDic["lat"] + "," + beginDic["lng"]; // 添加起始地址经纬度
            url += "&to=";
            url += endDic["lat"] + "," + endDic["lng"];  // 添加终止地址经纬度

            HttpClient request = new HttpClient();
            string result = request.GetStringAsync(url).Result;
            // 返回字符串，由前端转换为json格式
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        // 根据用户输入的地址获取经度和纬度
        public Dictionary<string,string> GetLatAndLngByAddrress(string address)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();


            string url = "https://apis.map.qq.com/ws/geocoder/v1/?key=xxx&address=";

            if (!string.IsNullOrEmpty(address))
            {
                url += address;
            }
            else
            {
                url += "北京市海淀区彩和坊路海淀西大街74号";
            }

            HttpClient request = new HttpClient();
            string result = request.GetStringAsync(url).Result;
            var data = JsonConvert.DeserializeObject<GetLatAndLngModel>(result);
            //dict.Add("lnt",data.result.location)
            var location = data.result.location;
            foreach (var item in location)
            {
                dict.Add(item.Key, item.Value);
            }
            return dict;
        }


        // 根据 用户输入的IP获取详细地址
        public ActionResult GetLocationByIP(string ip)
        {
            // 1. 先根据IP获取经纬度
            string url = "https://apis.map.qq.com/ws/location/v1/ip?key=xxx&ip=";
            if (!string.IsNullOrEmpty(ip))
            {
                url += ip;
            }
            else
            {
                url += "106.75.164.15";
            }
            HttpClient request = new HttpClient();
            string result = request.GetStringAsync(url).Result;
            var data1 = JsonConvert.DeserializeObject<GetLatAndLngByIP>(result); 
            // 2. 得到经纬度
            Dictionary<string, string> dic = data1.result.location;
            var lat = dic["lat"];
            var lng = dic["lng"];

            // 根据经纬度获取具体信息
            var data = GetDataByLocation(lat, lng);
            return data;
            //return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Map()
        {
            return View();
        }

        public ActionResult Test()
        {
            List<string> list = new List<string>();
            list.Add("df");
            list.Add("df");
            list.Add("df");
            return Json(new { Id = 3, Name = "yjc", Data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}
