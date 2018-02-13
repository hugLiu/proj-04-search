using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jurassic.WebFrame;

namespace Jurassic.So.SooilApp.Controllers
{
    /// <summary>
    /// 搜索相关配置
    /// </summary>
    [AllowAnonymous]
    public class SearchConfigController : BaseController
    {
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }
    }
}