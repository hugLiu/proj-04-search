/****************************************************************************
* Copyright @ 武汉侏罗纪技术开发有限公司 2017. All rights reserved.
* 
* 文 件 名: Class1
* 创 建 者：zhoush
* 创建日期：2017/6/14 9:33:56
* 功能描述: 
* 
* 修 改 人：    
* 修改时间:     
* 修改日志:    
*
* 审 查 者:     
* 审 查 时 间:  
****************************************************************************/

namespace Jurassic.So.Application.SearchConfig.Filter
{
    /// <summary>
    /// 左侧或右侧过滤条件[通常是聚合字段过滤]
    /// </summary>
    public class SideFilter : ISideFilter
    {
        /// <summary>
        /// ES中对于字段路径
        /// </summary>
        public string MappingField { get; set; }
        /// <summary>
        /// 显示数量
        /// </summary>
        public int Top { get; set; }
        public string TemplateName { get; set; }
        public string Title { get; set; }
        public FilterPosition FilterPosition { get; set; }
    }
}
