/****************************************************************************
* Copyright @ 武汉侏罗纪技术开发有限公司 2017. All rights reserved.
* 
* 文 件 名: HtmlSegmentTemplate
* 创 建 者：zhoush
* 创建日期：2017/6/15 8:59:06
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

namespace Jurassic.So.Application.SearchConfig.Config
{
    /// <summary>
    /// html片段模板
    /// </summary>
    public class HtmlSegmentTemplate
    {
        public HtmlSegmentTemplate(string filePath)
        {
            FilePath = filePath;
        }
        /// <summary>
        /// 模板类别名称，比如SliderFilter或searchitem
        /// </summary>
        public string TemplateCategoryName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(TemplateFullName))
                    return string.Empty;
                return TemplateFullName.Split(new char[] {'_', '-'}, StringSplitOptions.RemoveEmptyEntries)[0];
            }
        }

        public string TemplateName {
            get
            {
                if (string.IsNullOrWhiteSpace(TemplateFullName))
                    return string.Empty;
                var names=TemplateFullName.Split(new char[] {'_', '-'}, StringSplitOptions.RemoveEmptyEntries);
                return names.Length > 1 ? names[1] : TemplateFullName;
            }
        }
        public string Html { get; set; }
        public string TemplateFullName { get; set; }
        public string FilePath { get; set; }
        /// <summary>
        /// 效果图路径
        /// </summary>
        public string EffectImgPath { get; set; }
        public void Load()
        {
            if (!string.IsNullOrWhiteSpace(FilePath) && File.Exists(FilePath))
            {
                TemplateFullName = Path.GetFileNameWithoutExtension(FilePath);
                Html = File.ReadAllText(FilePath);
            }     
        }

        public void Save()
        {
            File.AppendAllText(FilePath,Html);
        }
    
    }
}
