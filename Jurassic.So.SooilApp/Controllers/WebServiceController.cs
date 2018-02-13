using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Aspose.Pdf;
using Jurassic.AppCenter;
using Jurassic.So.GeoTopic.Web.Utility;
using Jurassic.So.Infrastructure.Util;
using Jurassic.WebFrame;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using Jurassic.Sooil.IServiceBase;
//using Jurassic.Sooil.WebApp.Utility;

namespace Jurassic.Sooil.WebApp.Controllers
{
    /// <summary>
    /// WebApp外部服务获取统一方法
    /// </summary>
    [Authorize]
    public class WebServiceController : BaseController
    {
        //
        // GET: /WebService/
        private readonly string _apiPath = System.Configuration.ConfigurationManager.AppSettings["ApiServiceURL"] + System.Configuration.ConfigurationManager.AppSettings["ApiVersion"];
        //语义服务
        private readonly string _semanticsService = System.Configuration.ConfigurationManager.AppSettings["SemanticsService"];
        private readonly string _getDictionary = System.Configuration.ConfigurationManager.AppSettings["GetDictionary"];
        //分词服务
        private readonly string _segmentAnalyzer = System.Configuration.ConfigurationManager.AppSettings["SegmentAnalyzer"];
        //语义推荐
        private readonly string _semanticsRecomment = System.Configuration.ConfigurationManager.AppSettings["SemanticsRecomment"];
        private readonly string _hierarchy = System.Configuration.ConfigurationManager.AppSettings["Hierarchy"];
        private readonly string _semantics = System.Configuration.ConfigurationManager.AppSettings["Semantics"];
        //搜索服务
        private readonly string _searchService = System.Configuration.ConfigurationManager.AppSettings["SearchService"];
        //搜索
        private readonly string _search = System.Configuration.ConfigurationManager.AppSettings["Search"];
        private readonly string _infoRecomment = System.Configuration.ConfigurationManager.AppSettings["InfoRecomment"];
        private readonly string _getMetaData = System.Configuration.ConfigurationManager.AppSettings["GetMetaData"];
        private readonly string _getInfoItermByIds = System.Configuration.ConfigurationManager.AppSettings["GetInfoItermByIds"];
        //高级搜索
        private readonly string _advancedSearch = System.Configuration.ConfigurationManager.AppSettings["AdvancedSearch"];
        //相关搜索
        private readonly string _relatedSearch = System.Configuration.ConfigurationManager.AppSettings["RelatedSearch"];
        private readonly string _bOServiceUrl = System.Configuration.ConfigurationManager.AppSettings["BOServiceUrl"];
        private readonly string _bOSpatialUrl = System.Configuration.ConfigurationManager.AppSettings["BOSpatialUrl"];
        private readonly string _associatedBoUrL = System.Configuration.ConfigurationManager.AppSettings["AssociatedBOURL"];
        private readonly string _bOTreeUrL = System.Configuration.ConfigurationManager.AppSettings["BOTreeURL"];
        //数据服务
        private readonly string wcfKey = System.Configuration.ConfigurationManager.AppSettings["WcfKey"];
        private readonly string _dataService = System.Configuration.ConfigurationManager.AppSettings["DataService"];
        private readonly string _retrieve = System.Configuration.ConfigurationManager.AppSettings["Retrieve"];
        private readonly string _datasources = System.Configuration.ConfigurationManager.AppSettings["Datasources"];
        private readonly string _CookieFilePower = System.Configuration.ConfigurationManager.AppSettings["CookieFilePower"];


        [HttpPost]
        public JsonResult PostFromBody()
        {
            string pram = Request.Form["pram"];
            string url = Request.Form["url"];
            string userToken = TokenManmge.GetTokenService();
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()       
                {
                  {"", pram}               
                });
            return Json(WebRequestUtil.PostHttpClientStr(url, content, userToken).GetAwaiter().GetResult(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Post()
        {
            string pram = Request.Form["pram"];
            string url = Request.Form["url"];
            string userToken = TokenManmge.GetTokenService();
            return Json(WebRequestUtil.PostHttpClientStr(url, pram, userToken).GetAwaiter().GetResult(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult Get(string pram, string url)
        {
            var parmOdj = JsonConvert.DeserializeObject<Dictionary<string, string>>(pram);
            string userToken = TokenManmge.GetTokenService();
            return Json(WebRequestUtil.GetHttpClientStr(url, parmOdj, userToken).GetAwaiter().GetResult(), JsonRequestBehavior.AllowGet);
        }

        [Obsolete]
        public async Task<string> GetInfoItermByIds(string userId, IEnumerable<string> htList)
        {
            var pram = new List<KeyValuePair<string, string>>();
            pram.Add(new KeyValuePair<string, string>("Category", "SRC&SMD"));
            foreach (var item in htList)
            {
                pram.Add(new KeyValuePair<string, string>("Iids", item));
            }
            string userToken = TokenManmge.GetTokenService();
            var jsonlist = WebRequestUtil.PostHttpClientStr(_apiPath + _searchService + _getInfoItermByIds, new FormUrlEncodedContent(pram), userToken).GetAwaiter().GetResult();
            return await Task.FromResult(jsonlist);
        }

        public async Task<string> GetDictionary(string userId,Dictionary<string, string> pram)
        {
            string userToken = TokenManmge.GetTokenService();
            return await Task.FromResult(WebRequestUtil.PostHttpClientStr(_apiPath + _semanticsService + _getDictionary, pram, userToken).GetAwaiter().GetResult());
        }

        public async Task<string> GetMetaData(string userId, IEnumerable<string> iiids)
        {
            //string str = "[";
            //foreach (var item in iiids)
            //{
            //    str += item;
            //}
            //str += "]";
            var pram = new List<KeyValuePair<string, string>>();
            pram.Add(new KeyValuePair<string, string>("Category", "ForDetail"));
            foreach (var item in iiids)
            {
                pram.Add(new KeyValuePair<string, string>("Iids", item));
            }
            //pram.Add(new KeyValuePair<string, string>("Iids", str));
            string userToken = TokenManmge.GetTokenService();
            return await Task.FromResult(WebRequestUtil.PostHttpClientStr(_apiPath + _searchService + _getMetaData, new FormUrlEncodedContent(pram), userToken).GetAwaiter().GetResult());
        }

        public async Task<string> Retrieve(string userId, string src)
        {
            string userToken = TokenManmge.GetTokenService();
            var pram = new Dictionary<string, string>();
            pram.Add("key", wcfKey);
            pram.Add("src", src);
            pram.Add("schema", null);
            return await Task.FromResult(WebRequestUtil.GetHttpClientStr(_apiPath + _dataService + _retrieve, pram, userToken).GetAwaiter().GetResult());
        }

        public async Task<string> CookieFilePower(string userId, string adapterId)
        {
            string userToken = TokenManmge.GetTokenService();
            var pram = new Dictionary<string, string>();
            pram.Add("AdapterId", adapterId);
            return await Task.FromResult(WebRequestUtil.GetHttpClientStr(_apiPath + _dataService + _CookieFilePower, pram, userToken).GetAwaiter().GetResult());
        }

        public async Task<string> Hierarchy(string userId, string jsonparam)
        {
            string userToken = TokenManmge.GetTokenService();
            //var jsonparam = JsonConvert.DeserializeObject(jsonData);
            return await Task.FromResult(WebRequestUtil.PostHttpClientStr(_apiPath + _semanticsService + _hierarchy, jsonparam, userToken).GetAwaiter().GetResult());
            //return null;
        }
    }
}
