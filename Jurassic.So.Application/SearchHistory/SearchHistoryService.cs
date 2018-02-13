using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Jurassic.So.Application.SearchHistory.Dto;
using Jurassic.So.Domain.Models;
using Jurassic.So.Oracle;


namespace Jurassic.So.Application.SearchHistory
{
    public class SearchHistoryService : ISearchHistoryService
    {
        SooilDbContext dbcontext = new SooilDbContext();
        public void SaveLog(Guid pageid, Sooil_Search_History searchHistoryModel)
        {
            var sooilsearchModel = dbcontext.Sooil_Search_History.Where(o => o.SourceId == pageid.ToString());
            if (dbcontext.Sooil_Search_History.Any(o => o.SourceId == pageid.ToString()))
            {
                dbcontext.Sooil_Search_History.RemoveRange(sooilsearchModel);
                dbcontext.Sooil_Search_History.Add(searchHistoryModel);
            }
            else
            {
                try
                {
                    dbcontext.Sooil_Search_History.Add(searchHistoryModel);
                    dbcontext.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    dbcontext.SaveChanges();
                }
            }
        }


        public List<Sooil_Search_History> GetSearchHistory(Guid pageid)
        {
            List<Sooil_Search_History> historylist = null;
            historylist = dbcontext.Sooil_Search_History.Where(o => o.SourceId == pageid.ToString()).ToList();
            return historylist;
        }

        /// <summary>
        /// 根据当前用户ID获取历史信息 loginTime倒序排序，InputWord去重复  3天内
        /// </summary>
        /// <param name="pageid"></param>
        /// <returns></returns>
        public string[] GetInputWordByUserId(string userId)
        {
            DateTime date = DateTime.Today.AddDays(-3);
            string sqlStr = "select * from (select *,row_number() over(partition by InputWord order by Id desc)oid from Sooil_Search_History ";
            sqlStr += "where UserId='"+userId+"' and SourceTime>='"+date+"' and SourceWayEnum='Search'";
            sqlStr += "and InputWord!='' and InputWord is not null and IsDelete = 'false')A where oid=1";
            var historylist = dbcontext.Sooil_Search_History.SqlQuery(sqlStr).ToList();
            var history=historylist.OrderByDescending(o=>o.Id).Select(o=>o.InputWord.Trim()).ToArray();
            return history;
        }

        /// <summary>
        /// 根据登录ID获取搜索时间及其次数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string[] GetHistoryLoginTime(string userId, DateTime startTime)
        {
            //BaseInfoModel model = new BaseInfoModel();
            //var timeStr = (
            // from a in dbcontext.Sooil_Search_History
            // where a.UserId == userId
            // where a.SourceTime >= startTime && a.SourceWayEnum == "Search"
            // where a.InputWord != null && a.InputWord != ""
            // group a by (SqlFunctions.DateName("YYYY", a.SourceTime) + "-" +
            // SqlFunctions.DateName("MM", a.SourceTime) + "-" + SqlFunctions.DateName("DD", a.SourceTime)) into g
            // select g.Key + "," + g.Count().ToString()
            // ).ToArray();
            //return timeStr;
            return null;
        }

        /// <summary>
        /// 根据登录ID和时间获取历史记录信息  7天内的
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public List<Sooil_Search_History> GetSearchHistoryList(string userId,int pageIndex,int PageCount)
        {
            DateTime date = DateTime.Today.AddDays(-7);
            List<Sooil_Search_History> lst = null;
            lst = dbcontext.Sooil_Search_History.Where(o => o.UserId == userId && o.IsDelete == false &&
            o.SourceWayEnum == "Search" && o.InputWord != null && o.InputWord != "" && o.SourceTime >= date).OrderByDescending(o => o.SourceTime)
            .Skip((pageIndex-1)*PageCount).Take(PageCount).ToList();
            return lst;
        }

        /// <summary>
        /// 获取搜索排名前10的InputWord  //----7天内的
        /// </summary>
        /// <returns></returns>
        public List<SearchHistoryModel> GetHotSearchWord(int topCount)
        {
            DateTime date = DateTime.Today.AddDays(-7);
            List<Sooil_Search_History> lst;
            lst = dbcontext.Sooil_Search_History.Where(o => o.InputWord != null && o.InputWord != "" && o.SourceTime >= date).ToList();
            List<SearchHistoryModel> result = lst.GroupBy(x => x.InputWord.Trim())
                                .Select(g => new SearchHistoryModel
                                {
                                    word = g.Key,
                                    num = g.Count()
                                })
                                .OrderByDescending(x => x.num)
                                .Take(topCount).ToList();
            return result;
        }

        /// <summary>
        /// 保存基本的历史记录信息
        /// </summary>
        /// <param name="searhHistoryModel">基本历史记录信息的实体模型</param>
        /// <param name="isStartPage">是否是起始页</param>
        public void SaveClickLog(Sooil_Search_History searhHistoryModel, string isStartPage)
        {
            dbcontext.Sooil_Search_History.Add(searhHistoryModel);
            dbcontext.SaveChanges();
        }

        public void SaveLoginLog(Sooil_Search_History searhHistoryModel, string isStartPage)
        {
            Sooil_Search_History historylist = new Sooil_Search_History();
            historylist = dbcontext.Sooil_Search_History.FirstOrDefault(o => o.TargetId == searhHistoryModel.TargetId);
            if (historylist == null)
            {
                return;
            }
            historylist.TargetPageNameEnum = searhHistoryModel.TargetPageNameEnum;
            historylist.PageResultsEnum = searhHistoryModel.PageResultsEnum;
            TimeSpan ts = new TimeSpan();
            // DateTime startTime = Convert() searhHistoryModel.TargetTime.ToString();
            historylist.TargetTime = DateTime.Now;
            ts = Convert.ToDateTime(historylist.TargetTime) - Convert.ToDateTime(historylist.SourceTime);
            historylist.RunTime = ts.Minutes * 60 + ts.Seconds + Convert.ToDouble(ts.Milliseconds) / 1000;
            historylist.TargetPageNameEnum = searhHistoryModel.TargetPageNameEnum;
            if (searhHistoryModel.TargetPageNameEnum.Equals("SearchResultDetailed"))
            {
                historylist.BO = searhHistoryModel.BO;
                historylist.BOT = searhHistoryModel.BOT;
                historylist.PT = searhHistoryModel.PT;
                historylist.BP = searhHistoryModel.BP;
                historylist.Iiid = searhHistoryModel.Iiid;
                historylist.Title = searhHistoryModel.Title;
                //historylist.AdapterName = searhHistoryModel.AdapterName;
                historylist.ResourcesName = searhHistoryModel.ResourcesName;
                historylist.ResourcesFormat = searhHistoryModel.ResourcesFormat;
            }
            dbcontext.SaveChanges();
        }

        //根据传过来的ID数组，改变多个IsDelete的值
        public void UpdateIsDelete(List<int> idStr)
        {
            for (int i = 0; i < idStr.Count; i++)
            {
                dbcontext.Sooil_Search_History.Find(idStr[i]).IsDelete = true;
            }
            dbcontext.SaveChanges();
        }

        /// <summary>
        /// 保存历史记录的环境变量的信息
        /// </summary>
        /// <param name="searchHistoryData">历史记录的环境变量的实体数据</param>
        public void SavaEnvData(Sooil_History_Data searchHistoryData)
        {
            dbcontext.Sooil_History_Data.Add(searchHistoryData);
            dbcontext.SaveChanges();
        }

        public int GetHistoryListCount(string userId) 
        {
            int count=0;
            DateTime date = DateTime.Today.AddDays(-7);
            count = dbcontext.Sooil_Search_History.Where(o => o.UserId == userId && o.IsDelete == false &&
            o.SourceWayEnum == "Search" && o.InputWord.Trim() != null && o.InputWord.Trim() != "" && o.SourceTime >= date).Count();
            return count;
        }
    }
}

