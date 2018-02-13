using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jurassic.AppCenter;
using Jurassic.Com.Tools;
using Jurassic.So.Application.User;
using Jurassic.WebFrame;

namespace Jurassic.So.SooilApp.Controllers
{
    public class WebAppController : BaseController
    {
        private IUserApplication _userApplication;
        public WebAppController()
        {
            _userApplication = new UserApplication();
        }
        /// <summary>
        /// 主界面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {

            if (User != null)
            {
                var userSet = _userApplication.FindUserSettingDto(CommOp.ToInt(CurrentUserId));
                if (userSet != null)
                {
                    Response.Cookies["IsInfo"].Value = userSet.IsInfo.ToString();
                    Response.Cookies["IsInfo"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["Count"].Value = userSet.Count.ToString();
                    Response.Cookies["Count"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["IsPreview"].Value = userSet.IsPreview.ToString();
                    Response.Cookies["IsPreview"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["ShowHistory"].Value = userSet.ShowHistory.ToString();
                    Response.Cookies["ShowHistory"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["Language"].Value = userSet.Language;
                    Response.Cookies["Language"].Expires = DateTime.Now.AddDays(1);
                    //Response.Cookies["SearchMethod"].Value = userSet.SearchMethod;
                    //Response.Cookies["SearchMethod"].Expires = DateTime.Now.AddDays(1);
                }
                else
                {
                    Response.Cookies["IsInfo"].Value = "1";
                    Response.Cookies["IsInfo"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["Count"].Value = "10";
                    Response.Cookies["Count"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["IsPreview"].Value = "0";
                    Response.Cookies["IsPreview"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["ShowHistory"].Value = "1";
                    Response.Cookies["ShowHistory"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["Language"].Value = "zh-CN";
                    Response.Cookies["Language"].Expires = DateTime.Now.AddDays(1);
                    //Response.Cookies["SearchMethod"].Value = "0";
                    //Response.Cookies["SearchMethod"].Expires = DateTime.Now.AddDays(1);
                }

            }
            //请求的地方只有3处，要是多的话可以考虑放入Cookie，Cookie中有维持同步和泄露用户名的危险。
            if (User != null)
            {
                var model = _userApplication.GetSooilUserInfoByUserId(CurrentUserId);
                if (model != null)
                {
                    Session["Name"] = model.Name;
                }
            }
            ViewBag.IsUser = !string.IsNullOrWhiteSpace(User.Identity.GetUserId());
            ViewBag.UserName = User.Identity.GetUserName();
            if (!string.IsNullOrEmpty(Session["Name"] as string))
            {
                ViewBag.UserName = Session["Name"] as string;
            }
            return View();
        }

        public ActionResult SearchIndex()
        {
            if (User != null)
            {
                var userSet = _userApplication.FindUserSettingDto(CommOp.ToInt(CurrentUserId));
                if (userSet != null)
                {
                    Response.Cookies["IsInfo"].Value = userSet.IsInfo.ToString();
                    Response.Cookies["IsInfo"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["Count"].Value = userSet.Count.ToString();
                    Response.Cookies["Count"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["IsPreview"].Value = userSet.IsPreview.ToString();
                    Response.Cookies["IsPreview"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["ShowHistory"].Value = userSet.ShowHistory.ToString();
                    Response.Cookies["ShowHistory"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["Language"].Value = userSet.Language;
                    Response.Cookies["Language"].Expires = DateTime.Now.AddDays(1);
                    //Response.Cookies["SearchMethod"].Value = userSet.SearchMethod;
                    //Response.Cookies["SearchMethod"].Expires = DateTime.Now.AddDays(1);
                }
                else
                {
                    Response.Cookies["IsInfo"].Value = "1";
                    Response.Cookies["IsInfo"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["Count"].Value = "10";
                    Response.Cookies["Count"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["IsPreview"].Value = "0";
                    Response.Cookies["IsPreview"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["ShowHistory"].Value = "1";
                    Response.Cookies["ShowHistory"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["Language"].Value = "zh-CN";
                    Response.Cookies["Language"].Expires = DateTime.Now.AddDays(1);
                    //Response.Cookies["SearchMethod"].Value = "0";
                    //Response.Cookies["SearchMethod"].Expires = DateTime.Now.AddDays(1);
                }

            }
            //请求的地方只有3处，要是多的话可以考虑放入Cookie，Cookie中有维持同步和泄露用户名的危险。
            if (User != null)
            {
                var model = _userApplication.GetSooilUserInfoByUserId(CurrentUserId);
                if (model != null)
                {
                    Session["Name"] = model.Name;
                }
            }
            ViewBag.IsUser = !string.IsNullOrWhiteSpace(User.Identity.GetUserId());
            ViewBag.UserName = User.Identity.GetUserName();
            if (!string.IsNullOrEmpty(Session["Name"] as string))
            {
                ViewBag.UserName = Session["Name"] as string;
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult SearchResult()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult SearchResult2()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SearchResult3()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SearchResult4()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult SearchResultDetail()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult AdvanceSearch()
        {
            return View();
        }

        public ActionResult SearchResultDetailOld()
        {
            return View();
        }
    }

  
}