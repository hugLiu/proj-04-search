/****************************************************************************
* Copyright @ 武汉侏罗纪技术开发有限公司 2017. All rights reserved.
* 
* 文 件 名: FieldMetadata
* 创 建 者：zhoush
* 创建日期：2017/6/14 10:30:40
* 功能描述: 
* 
* 修 改 人：    
* 修改时间:     
* 修改日志:    
*
* 审 查 者:     
* 审 查 时 间:  
****************************************************************************/

using Newtonsoft.Json;

namespace Jurassic.So.Application.Metadata
{
    public class MetadataDefinition
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("mapping")]
        public object Mapping { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("required")]
        public bool Required { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("format")]
        public string Format { get; set; }
        [JsonProperty("innerTag")]
        public bool InnerTag { get; set; }

        [JsonProperty("groupName")]
        public string GroupName { get; set; }
        [JsonProperty("uitype")]
        public string UiType { get; set; }
        [JsonIgnore,JsonProperty("items")]
        public string[] Items { get; set; }
        [JsonProperty("showindetail")]
        public bool ShowInDetail { get; set; }

        [JsonIgnore]
        public string FieldMapping
        {
            get { return string.Empty; }
        }

    }
}
