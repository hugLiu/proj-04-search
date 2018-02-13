using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Jurassic.AppCenter;
using Jurassic.AppCenter.Resources;
using Jurassic.So.Application.SearchHistory;
//using Jurassic.Sooil.WebApp.Controllers;

namespace Jurassic.So.SooilApp.Controllers
{
    //补全记录一共10条，搜索历史2条，其他为词库。
    //[Authorize]
    public class TypeAheadController : Controller//BaseController
    {
        //应该放到页面缓存中去，页面加载时延时加载。每次页面加载重新获取。
        //每隔1h刷新一下历史，每隔24h刷新一下词库
        //termList用于循环，并未用于检索Key，所以数组的效率是最高的。
        public static string[] termList = null;//下拉提示term缓存 -词库部分
        public static DateTime termTime = DateTime.Now;
        private readonly string apiUrl = System.Configuration.ConfigurationManager.AppSettings["ApiServiceURL"]
            + System.Configuration.ConfigurationManager.AppSettings["ApiVersion"];

        public ISearchHistoryService HistoryServiceable { get; set; }
        public TypeAheadController(ISearchHistoryService services)
        {
            HistoryServiceable = services;
        }
        /// <summary>
        /// 获取下拉补全数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTypeAheadJson()
        {
            string query = Request["query"].Trim();
            string other = Request["other"];
            TimeSpan termTs = new TimeSpan();
            termTs = DateTime.Now - termTime;
            //if (string.IsNullOrEmpty(User.Identity.GetUserId()))
            //{
            //    return null;
            //}
            if (Session["historyList"] == null)
            {
                GetHistoryWord();
            }

            if (string.IsNullOrWhiteSpace(query) || query == "") //如果query ==""，直接显示搜索历史部分
            {
                return Json(GetStr(), JsonRequestBehavior.AllowGet);
            }
            //取空格后面的字串
            int pos = query.LastIndexOf(" ");
            string preTrem = query.Substring(0, pos + 1);
            string queryTerm = query.Substring(pos + 1).ToLower();
            if (termList == null || termTs.Hours >= 24)
            {
                GetTermList();
                if (termList == null)
                {
                    return Json(GetStr(queryTerm), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    termTime = DateTime.Now;//刷新词库时间
                }
            }
            List<Hashtable> listHt = GetStr(query, preTrem);
            return Json(listHt, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 加载词库和历史
        /// </summary>
        /// <returns></returns>
        public JsonResult LoginInfo()
        {
            if (string.IsNullOrEmpty(User.Identity.GetUserId()))
            {
                return null;
            }
            if (termList == null)
            {
                GetTermList();
            }
            GetHistoryWord();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取词库方法
        /// </summary>
        /// <returns></returns>
        public void GetTermList()
        {
            var urlPram = new Dictionary<string, string>();
            urlPram.Add("Type", "full");
            urlPram.Add("Synonymous", "false");
            urlPram.Add("Translation", "false");
            try
            {
                //string result = new WebServiceController().GetDictionary(User.Identity.GetUserId(), urlPram).Result;
                string result = string.Empty;
                if (!string.IsNullOrEmpty(result))
                {
                    termList = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(result);
                    Array.Sort(termList);
                    //var tokens = Newtonsoft.Json.Linq.JObject.Parse(result);
                    //if (tokens.HasValues)
                    //{
                    //    termList = Newtonsoft.Json.Linq.JArray.Parse(tokens["Data"].ToString().ToLower()).ToObject<List<string>>().ToArray();//将string转化为List<string>
                    //    Array.Sort(termList);
                    //}
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResHelper.GetStr("S_CM_Info_NoDictionnaryData"));
            }

        }

        /// <summary>
        /// 获取当前用户搜索历史信息
        /// </summary>
        /// <returns></returns>
        public void GetHistoryWord()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                Session["historyList"] = HistoryServiceable.GetInputWordByUserId(userId);
            }
            catch (Exception)
            {
                throw new Exception(ResHelper.GetStr("S_CM_Info_NoHistoryData"));
            }

        }

        /// <summary>
        /// 将得到的历史和词库数据 收集到补全集合
        /// </summary>
        /// <returns></returns>
        public List<Hashtable> GetStr(string queryTerm, string preTrem)
        {
            List<Hashtable> listHt = new List<Hashtable>();
            List<string> wordList = new List<string>();
            string[] historyList = Session["historyList"] as string[];
            Hashtable ht;

            int count = 0; //记录listHt有多少键值对


            if (historyList != null) //添加历史数组
            {
                var hisList = historyList.Where(o => o.StartsWith(queryTerm)).Take(2).ToArray();
                foreach (var term in hisList)
                {
                    count++;
                   
                    if (wordList.Contains(term))
                        continue;
                    ht = new Hashtable();
                    ht.Add("id", "" + count);
                    //if (term.ToString().Length > 25)
                    //    ht.Add("text", term.ToString().Substring(0, 25) + "ab$cd");
                    //else
                    //    ht.Add("text", term + "ab$cd");
                    if (term.ToString().Length > 25)
                        ht.Add("text", term.ToString().Substring(0, 25));
                    else
                        ht.Add("text", term);
                    wordList.Add(term);
                    listHt.Add(ht);
                    if (count == 2) break;
                }
            }

            if (termList != null) //添加词库数组
            {
                var tmList = termList.Where(o => o.StartsWith(queryTerm)).Take(10).ToArray();

                // Random rn = new Random();
                // int m = rn.Next(0,100*100*10);
                //// var a = queryTerm.Substring(0, 1);
                // //int index = i; //Array.BinarySearch(termList,a);
                // int indexFirst = m ;// Array.FindIndex(termList, item => item.StartsWith(queryTerm));
                // if (indexFirst<0)
                //     return listHt;
                // int indexEnd = m + 1000;//Array.FindLastIndex(termList, indexFirst,10, item => item.StartsWith(queryTerm));

                // //int sum = indexEnd + 1 - indexFirst;
                // //Array.Reverse(termList, indexFirst, sum);
                // //var a= termList;
                // //int list = Array.BinarySearch(termList,item=>item.Sta    rtsWith("11"));  m=8151

                foreach (var item in tmList)
                {
                    count++;
                    if (wordList.Contains(item))
                        continue;

                    ht = new Hashtable();
                    ht.Add("id", "" + count);
                    if (item.Length > 25)
                        ht.Add("text", item.Substring(0, 25));
                    else
                        ht.Add("text", item);
                    wordList.Add(item);
                    listHt.Add(ht);
                    if (count == 10) break;
                }
            }
            return listHt;
        }

        /// <summary>
        /// 当 query 为空值时，取10条历史记录,若是没有找到匹配的历史记录取前三条记录
        /// </summary>
        /// <param name="queryTerm"></param>
        /// <returns></returns>
        public List<Hashtable> GetStr(string queryTerm)
        {
            List<Hashtable> listHt = new List<Hashtable>();
            string[] historyList = Session["historyList"] as string[];
            Hashtable ht;
            int count = 0;
            foreach (var item in historyList)
            {
                if (item.ToLower().StartsWith(queryTerm))
                {
                    count++;
                    ht = new Hashtable();
                    ht.Add("id", "" + count);
                    //if (item.Length > 25)
                    //    ht.Add("text", item.Substring(0, 25) + "ab$cd");
                    //else
                    //    ht.Add("text", item + "ab$cd");
                    if (item.Length > 25)
                        ht.Add("text", item.Substring(0, 25));
                    else
                        ht.Add("text", item);
                    listHt.Add(ht);
                    if (count == 10) break;
                }
            }
            return listHt;
        }

        /// <summary>
        /// 若是没有找到匹配的历史记录取前三条记录
        /// </summary>
        /// <param name="historyList"></param>
        /// <returns></returns>
        public List<Hashtable> GetStr()
        {
            List<Hashtable> listHt = new List<Hashtable>();
            Hashtable ht;
            string[] historyList = Session["historyList"] as string[];
            int count = 0;
            foreach (var item in historyList)
            {
                count++;
                ht = new Hashtable();
                ht.Add("id", "" + count);
                //if (item.Length > 25)
                //    ht.Add("text", item.Substring(0, 25) + "ab$cd");
                //else
                //    ht.Add("text", item + "ab$cd");
                if (item.Length > 25)
                    ht.Add("text", item.Substring(0, 25));
                else
                    ht.Add("text", item);
                listHt.Add(ht);
                if (count == 10) break;
            }
            return listHt;
        }
    }
}
