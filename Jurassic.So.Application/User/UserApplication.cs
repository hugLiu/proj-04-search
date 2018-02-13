/****************************************************************************
* Copyright @ 武汉侏罗纪技术开发有限公司 2017. All rights reserved.
* 
* 文 件 名: UserApplication
* 创 建 者：zhoush
* 创建日期：2017/5/5 11:20:52
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
using Jurassic.So.Application.User.Dto;
using Jurassic.So.Oracle;

namespace Jurassic.So.Application.User
{
    public class UserApplication: IUserApplication
    {
        public UserApplication()
        {
            
        }

        public UserSettingDto FindUserSettingDto(int userId)
        {
            return new UserSettingDto();
        }

        public SooilUserInfoDto GetSooilUserInfoByUserId(string userId)
        {
            return new SooilUserInfoDto();
        }
    }
}
