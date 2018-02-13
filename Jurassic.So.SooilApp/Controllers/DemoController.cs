using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jurassic.WebFrame;

namespace Jurassic.So.SooilApp.Controllers
{
    /// <summary>
    /// 控制器都应从Jurassic.WebFrame.BaseController继承
    /// 控制器都应加[Authorize]权限属性
    /// </summary>
    [AllowAnonymous]
    public class DemoController : BaseController
    {
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }
    }
}