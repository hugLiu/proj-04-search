/****************************************************************************
* Copyright @ 武汉侏罗纪技术开发有限公司 2017. All rights reserved.
* 
* 文 件 名: UserSetting
* 创 建 者：zhoush
* 创建日期：2017/5/5 11:22:17
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

namespace Jurassic.So.Application.User.Dto
{
    public class UserSettingDto
    {
      
        public string UserID { get; set; }

        public int? IsInfo { get; set; }

        public int? Count { get; set; }

        public int? IsPreview { get; set; }

        public int? ShowHistory { get; set; }

        
        public string Language { get; set; }

       
        public string SearchMethod { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        
        public string LastUpdatedBy { get; set; }

      
        public string Remark { get; set; }
    }
}
