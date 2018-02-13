using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Jurassic.So.SooilApp.App_Start
{
    public class WebRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes, params string[] namespaces)
        {
            routes.Remove(routes.LastOrDefault());
            routes.Remove(routes.LastOrDefault());
            routes.Remove(routes.LastOrDefault());
            routes.Remove(routes.LastOrDefault());

            routes.MapRoute(
               "Globalization_App", // 带区域性质的路由
               "{culture}/{controller}/{action}/{id}",
               new { controller = "WebApp", action = "Index", id = UrlParameter.Optional }, // 参数默认值
               new { culture = "^[a-zA-Z]{2}(-[a-zA-Z]{2,6})?$", controller = "^\\w{3,}\\d*$" },//参数约束
               namespaces
              );

            routes.MapRoute(
                "Globalization", // 带区域性质的路由
                "{culture}/{controller}/{action}/{id}",
                new { controller = "WebApp", action = "Index", id = UrlParameter.Optional }, // 参数默认值
                new { culture = "^[a-zA-Z]{2}(-[a-zA-Z]{2,6})?$", controller = "^\\w{3,}\\d*$" },//参数约束
                    namespaces
               );

            routes.MapRoute(
                "Default_App", // 默认路由
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "WebApp", action = "Index", id = UrlParameter.Optional }, // 参数默认值
                namespaces
            );

            routes.MapRoute(
                "Default", // 默认路由
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "WebApp", action = "Index", id = UrlParameter.Optional }, // 参数默认值
                    namespaces
            );
        }
    }
}