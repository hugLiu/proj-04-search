/****************************************************************************
* Copyright @ 武汉侏罗纪技术开发有限公司 2017. All rights reserved.
* 
* 文 件 名: MetadataDefinitionService
* 创 建 者：zhoush
* 创建日期：2017/6/15 9:48:26
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
using Jurassic.So.Search.ES;

namespace Jurassic.So.Application.Metadata
{
    public class MetadataDefinitionService : IMetadataDefinitionService
    {
        /// <summary>
        /// 获取所有元数据定义
        /// </summary>
        /// <returns></returns>
        public List<MetadataDefinition> GetAllMetadatadefines()
        {
            ESAccess<MetadataDefinition> esAccess=new ESAccess<MetadataDefinition>("MetadataDefinition");
            return esAccess.GetAllDocuments();
        }
    }
}
