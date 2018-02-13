/****************************************************************************
* Copyright @ 武汉侏罗纪技术开发有限公司 2017. All rights reserved.
* 
* 文 件 名: SoilUserInfo
* 创 建 者：zhoush
* 创建日期：2017/5/5 11:30:24
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
   public class SooilUserInfoDto
    {
      
        public string UserID { get; set; }

      
        public string LoginName { get; set; }

      
        public string Name { get; set; }

      
        public string Sex { get; set; }

       
        public byte[] Photo { get; set; }

     
        public byte[] PhotoThumbnail { get; set; }

    
        public DateTime? Birthday { get; set; }

     
        public string BirthPlace { get; set; }

      
        public string LifePlace { get; set; }

      
        public string Email { get; set; }

     
        public string Cellphone { get; set; }

   
        public string UserAccount { get; set; }

        public int? IsInvalid { get; set; }

      
        public string Remark { get; set; }
    }
}
