using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 位置搜索.Controllers
{
    // 用于调用腾讯地图API接口的
    public class MapController : Controller
    {

        // 显示以北京的天安门为中心的 603x300 地图
        public ActionResult Index()
        {
            return View();
        }

        // 在地图上绘制图形
        public ActionResult Index2()
        {
            return View();
        }

        // 普通关键字检索服务
        public ActionResult Index3()
        {
            return View();
        }

        // 根据客户端定位位置
        public ActionResult Index4()
        {
            return View();
        }
    }
}