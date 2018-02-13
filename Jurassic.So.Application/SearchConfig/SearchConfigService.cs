/****************************************************************************
* Copyright @ 武汉侏罗纪技术开发有限公司 2017. All rights reserved.
* 
* 文 件 名: SearchConfigService
* 创 建 者：zhoush
* 创建日期：2017/6/14 9:27:27
* 功能描述: 
* 
* 修 改 人：    
* 修改时间:     
* 修改日志:    
*
* 审 查 者:     
* 审 查 时 间:  
****************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jurassic.So.Application.SearchConfig.Config;
using Jurassic.So.Application.SearchConfig.Filter;
using Jurassic.So.Core.Util;

namespace Jurassic.So.Application.SearchConfig
{
    public class SearchConfigService : ISearchConfigService
    {
        private static string _appDir = AppDomain.CurrentDomain.BaseDirectory + "\\";
        private static string _searchIndexDir = AppDomain.CurrentDomain.BaseDirectory + "\\SearchConfig\\SearchIndex";
        private static string _searchResultDir = AppDomain.CurrentDomain.BaseDirectory + "\\SearchConfig\\SearchResult";
        private static string _searchDetailDir = AppDomain.CurrentDomain.BaseDirectory + "\\SearchConfig\\SearchDetail";
        public SearchResultConfig GetSearchResultConfig()
        {
            string searchConfigPath = _searchResultDir+"\\SearchResult.xml";
            if (!File.Exists(searchConfigPath))
                CreateDefaultSearchResultConfig();
            return XmlUtil.DeSerialize<SearchResultConfig>(searchConfigPath);
        }

        private void CreateDefaultSearchResultConfig()
        {
            string searchConfigPath = _searchResultDir + "\\SearchResult.xml";
            SearchResultConfig config=new SearchResultConfig();
            config.SearchItemTemplateName = "searchitem_default";
            config.SideFilters = new List<ISideFilter>();
            config.SideFilters.Add(new SideFilter() { MappingField = "ep.producttype",TemplateName ="siderfilter_default",Top=10,Title = "成果类型"});
            config.SideFilters.Add(new SideFilter() { MappingField = "dc.contributor", TemplateName = "siderfilter_default", Top = 10, Title = "作者" });
            config.SearchFields = new List<FieldTitle>();
            config.SearchFields.Add(new FieldTitle() {FieldName = "Title",MappingField = "dc.title",Title="标题"});
            config.SearchFields.Add(new FieldTitle() { FieldName = "Abstract", MappingField = "dc.abstract", Title = "摘要" });
            XmlUtil.Serialize(config,searchConfigPath);
        }
        public List<HtmlSegmentTemplate> LoadAllHtmlTemplate()
        {
            var templateDir =new DirectoryInfo(_searchResultDir + "\\segment");
            var files = templateDir.GetFiles("*.txt").Select(item => new HtmlSegmentTemplate(item.FullName));
            foreach (var segmentFile in files)
            {
                segmentFile.Load();
                var imgPath = templateDir.FullName + "\\" + segmentFile.TemplateFullName + ".jpg";
                if (File.Exists(imgPath))
                {
                    segmentFile.EffectImgPath = imgPath;
                }
            }
            return files.ToList();
        }
    }
}
