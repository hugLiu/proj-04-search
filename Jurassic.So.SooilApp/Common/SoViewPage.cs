using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jurassic.AppCenter;

namespace Jurassic.So.SooilApp.Common
{
    public abstract class SoWebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public string UserName
        {
            get
            {
                return AppManager.Instance.GetCurrentUserName();
                
            }
            
        }

        public bool HasLogin()
        {
            return !string.IsNullOrWhiteSpace(UserName);
        }
    }

    public abstract class SoWebViewPage : SoWebViewPage<dynamic>
    {

    }

}