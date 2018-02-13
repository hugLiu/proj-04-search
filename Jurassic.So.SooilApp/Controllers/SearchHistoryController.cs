using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Jurassic.AppCenter;
using Jurassic.AppCenter.Resources;
using Jurassic.So.Application.SearchHistory;
using Jurassic.So.Application.SearchHistory.Dto;
using Jurassic.So.Domain.Models;
using Jurassic.WebFrame;
using Newtonsoft.Json;

namespace Jurassic.So.SooilApp.Controllers
{
    [AllowAnonymous]
    public class SearchHistoryController : BaseController
    {
        //所有和用户行为相关的都放到BehaviorAnalysisController中，GetIp这种公用方法放在Jurassic.Sooil.SooilService的Infrastructure中
        public ISearchHistoryService searchServiceable { get; set; }
        public SearchHistoryController(ISearchHistoryService searchService)
        {
            searchServiceable = searchService;
        }

        public ActionResult SearchHistory()
        {
            ViewBag.UserName = User.Identity.GetUserName();
            if (!string.IsNullOrEmpty(Session["Name"] as string))
            {
                ViewBag.UserName = Session["Name"] as string;
            }
            return View();
        }

        //获得显示的数据
        public JsonResult GethidtoryList(string guid)
        {
            var newpageid = Guid.Parse(guid);
            var list = searchServiceable.GetSearchHistory(newpageid).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

       //获取数据总数 Count
        public JsonResult GetHistoryListCount()
        {
            int count=0;
            string userId = User.Identity.GetUserId();
            count = searchServiceable.GetHistoryListCount(userId);
            return Json(count, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取用户记录列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSearchHistoryList(int pageIndex,int pageCount)
        {
            string oldDate = "";
            string userId = User.Identity.GetUserId();
            List<Sooil_Search_History> list = searchServiceable.GetSearchHistoryList(userId, pageIndex, pageCount);
            SearchHistoryModel model;
            List<SearchHistoryModel> history = new List<SearchHistoryModel>();
            if (list.Count == 0)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
          
            for (int i = 0; i < list.Count; i++)
            {
                model = new SearchHistoryModel();
                model.id = list[i].Id.ToString();
                model.fdate = "";
                if (Convert.ToDateTime(list[i].SourceTime.ToString()).ToShortDateString() != oldDate)
                {
                    model.fdate = Convert.ToDateTime(list[i].SourceTime.ToString()).ToShortDateString();
                    oldDate = model.fdate;
                }

                if (model.fdate.Equals(DateTime.Now.ToShortDateString()))
                {
                    model.fdate = ResHelper.GetStr("S_SH_Label_Today");
                }
               
                model.ftime = Convert.ToDateTime(list[i].SourceTime.ToString()).ToShortTimeString();
                model.isDelete = list[i].IsDelete == true ? "true" : "false";
                model.word = list[i].InputWord.Trim();
                model.urlStr = "";
                history.Add(model);
            }
            return Json(history, JsonRequestBehavior.AllowGet);
        }

        //改变历史记录是否删除状态
        public JsonResult UpdateIsDelete(string str)
        {
            List<int> idStr = Newtonsoft.Json.Linq.JArray.Parse(str).ToObject<List<int>>();
            searchServiceable.UpdateIsDelete(idStr);
            return Json("", JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 获取用户IP
        /// </summary>
        /// <returns></returns>
        private string GetIp()
        {
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }


        #region 历史记录折线图方法，不要删


        ///// <summary>
        ///// 搜索历史数据 为date数组
        ///// </summary>
        ///// <param name="days"></param>
        ///// <returns></returns>
        //public JsonResult GetSearchHistoryCount(int days)
        //{
        //    string userId = User.Identity.GetUserId();
        //    DateTime startTime = new DateTime();
        //    string dateString = DateTime.Now.AddDays(-days + 1).ToShortDateString();
        //    DateTime[] dateAllStr = new DateTime[days];
        //    for (int i = 0; i < days; i++)
        //    {
        //        dateAllStr[i] = DateTime.Parse(DateTime.Now.AddDays(-days + 1 + i).ToShortDateString());//折线图所有下标日期数组
        //    }
        //    startTime = Convert.ToDateTime(dateString);
        //    //按时间升序排列的数组
        //    DateTime[] dateStr = searchServiceable.GetHistoryLoginTimeNode(userId, startTime);//数据库返回的日期数组
        //    int[] countArray = new int[days];
        //    for (int i = 0; i < countArray.Length; i++)
        //    {
        //        countArray[i] = 0;
        //    }
        //    for (int i = 0; i < dateAllStr.Length; i++)
        //    {
        //        for (int j = 0; j < dateStr.Length; j++)
        //        {
        //            DateTime date = DateTime.Parse(dateStr[j].ToShortDateString());
        //            if (dateAllStr[i] == date)
        //                countArray[i]++;
        //            if (dateAllStr[i] < date)
        //                break;
        //        }
        //    }
        //    return Json(countArray, JsonRequestBehavior.AllowGet);
        //}
        #endregion


        /// <summary>
        ///  搜索历史数据 为date数组+个数
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public JsonResult GetSearchHistoryCount(int days)
        {
            string userId = User.Identity.GetUserId();
            DateTime startTime = new DateTime();
            string dateString = DateTime.Now.AddDays(-days + 1).ToShortDateString();
            string[] dateAllStr = new string[days];
            for (int i = 0; i < days; i++)
            {
                dateAllStr[i] = DateTime.Now.AddDays(-days + 1 + i).ToShortDateString();//折线图所有下标日期数组
            }
            startTime = Convert.ToDateTime(dateString);
            int[] numStr = new int[days];
            for (int i = 0; i < numStr.Length; i++)
            {
                numStr[i] = 0;
            }
            string[] str = searchServiceable.GetHistoryLoginTime(userId, startTime);
            string[] sa = new string[2];
            for (int i = 0; i < dateAllStr.Length; i++)
            {
                for (int j = 0; j < str.Length; j++)
                {
                    sa = str[j].Split(',');
                    if (DateTime.Parse(sa[0]).ToShortDateString().Equals(dateAllStr[i]))
                        numStr[i] = int.Parse(sa[1]);
                }
            }

            return Json(numStr, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 检索历史的时间轴
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchHistoryList()
        {
            return View();
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveLogResult(string model)
        {
            string userId = User.Identity.GetUserId();
            //if (string.IsNullOrEmpty(userId))
            //{
            //    return Json("", JsonRequestBehavior.AllowGet);
            //}
            var searchHistory = JsonConvert.DeserializeObject<Sooil_Search_History>(model);
            if (!string.IsNullOrEmpty(searchHistory.SourceWayEnum))
            {
                searchHistory.SourceTime = DateTime.Now;
                searchHistory.ClientIP = GetIp();
                searchHistory.BrowserName = Request.Browser.Type;
                searchHistory.UserId = userId;
                searchServiceable.SaveClickLog(searchHistory, "false");
            }
            else
            {
                searchServiceable.SaveLoginLog(searchHistory, "false");
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SavaEnvData(string pageId, string data)
        {
            string userId = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            Sooil_History_Data model = new Sooil_History_Data();
            if (!string.IsNullOrEmpty(pageId))
            {
                model.HistoryId = Guid.Parse(pageId);
                model.HistoryData = data;
                searchServiceable.SavaEnvData(model);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }


            return Json("", JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 获取前10热词列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetHotSearchWord(int topCount=5) 
        {
            List<SearchHistoryModel> model = searchServiceable.GetHotSearchWord(topCount);
            return Json(model, JsonRequestBehavior.AllowGet); 
        }
    }
}
