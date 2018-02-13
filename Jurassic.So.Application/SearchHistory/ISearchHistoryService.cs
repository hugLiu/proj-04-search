using System;
using System.Collections.Generic;
using Jurassic.So.Application.SearchHistory.Dto;
using Jurassic.So.Domain.Models;

namespace Jurassic.So.Application.SearchHistory
{
    public interface ISearchHistoryService
    {
        void SaveLog(Guid pageid,Sooil_Search_History searchHistoryModel);
        List<Sooil_Search_History> GetSearchHistory(Guid pageid);
        string[] GetInputWordByUserId(string userId);
        string[] GetHistoryLoginTime(string userId,DateTime startTime);
        List<Sooil_Search_History> GetSearchHistoryList(string userId,int pageIndex,int PageCount);

        //重载保存日志的方法  点击事件
         ///searhHistoryModel:基本的保存的数据  
         ///isStartPage：是否是起始页   
        void SaveClickLog(Sooil_Search_History searhHistoryModel, string isStartPage);

        //  加载事件
        void SaveLoginLog(Sooil_Search_History searhHistoryModel, string isStartPage);
        //保存历史记录的环境变量
        ///searchHistoryData:保存环境变量的模型
        void SavaEnvData(Sooil_History_Data searchHistoryData);

        void UpdateIsDelete(List<int> idStr);//改变历史记录表 IsDelete状态

        List<SearchHistoryModel> GetHotSearchWord(int topCount);

        int GetHistoryListCount(string userId);
    }
}
