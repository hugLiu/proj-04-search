/****************************************************************************
* Copyright @ 武汉侏罗纪技术开发有限公司 2017. All rights reserved.
* 
* 文 件 名: SearchResultConfig
* 创 建 者：zhoush
* 创建日期：2017/6/14 9:29:30
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jurassic.So.Application.SearchConfig.Config
{
    public class SearchResultConfig
    {/// <summary>
    /// 边侧过滤条件配置
    /// </summary>
        public List<ISideFilter> SideFilters { get; set; }
        /// <summary>
        /// 搜索数据每项显示模板
        /// </summary>
        public string SearchItemTemplateName { get; set; }
        /// <summary>
        /// 模糊匹配需要匹配哪些字段
        /// </summary>
        public List<FieldTitle> SearchFields { get; set; }
    }

    public class FieldTitle
    {
        public string FieldName { get; set; }
        public string MappingField { get; set; }
        public string Title { get; set; }
    }
   
}
