using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Jurassic.CommonModels.EFProvider;
using Jurassic.So.Application.Metadata;
using Jurassic.So.Application.SearchConfig;
using Jurassic.So.Application.SearchHistory;
using Jurassic.So.Application.User;
//using Jurassic.So.GeoTopic.Web;
using Jurassic.So.Oracle;
using Ninject;
using Jurassic.So.SooilApp.App_Start;
using Jurassic.WebFrame;
using Ninject.Web.Common;

namespace Jurassic.So.SooilApp
{
    /// <summary>
    /// 应用程序必须继承自GTApplication
    /// </summary>
    public class DemoApplication : MvcApplication
    {
        //protected override IEnumerable<string> ControllerNameSpaces
        //{
        //    get
        //    {
        //        var list = base.ControllerNameSpaces.ToList();
        //        //声明自身Controller所在的命名空间
        //        list.Insert(0, typeof(DemoApplication).Namespace + ".Controllers");
        //        return list;
        //    }
        //}

        protected override void Application_Start()
        {
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
            base.Application_Start();

            //Todo: 额外的初始化代码
            //WebRouteConfig.RegisterRoutes(RouteTable.Routes, ControllerNameSpaces.ToArray());
        }

        /// <summary>
        /// 加入注入服务绑定
        /// </summary>
        protected override void AddBindings(IKernel ninjectKernel)
        {
            base.AddBindings(ninjectKernel);
            var databaseUser = ConfigurationManager.AppSettings["DatabaseUser"];

            ninjectKernel.Rebind<DbContext, ModelContext>().To<ModelContext>()
            .InRequestScope() //在一个请求中只生成一个Context, webframe 3.0新增
                              //.InThreadScope()
                              // 以下的一句代码是连接数据库时声明数据库的Schema.
                              // 其中"WEBFRAME"是Oracle库的Schema名称，如果直接运行WebFrame,它默认是连接oracle库,如果运行WebTemplate,它是连sqlserver
                              //.WithPropertyValue("Schema", "PMDB");
               .WithPropertyValue("Schema", databaseUser);
            //要支持Oralce数据库，请在""中填写Oralce库的Schema名称
            ninjectKernel.Rebind<SooilDbContext>().To<SooilDbContext>()
               .WithPropertyValue("Schema", databaseUser);

            //如果要修改上传根目录，请恢复以下代码,并修改第二个参数
            //ninjectKernel.Rebind<IFileLocator>().To(typeof(FileLocator))
            //       .WithConstructorArgument("rootPath", "D:\\Temp"); 

            //如果要开启多标签，或修改默认皮肤，请恢复以下代码,并修改第二个参数
            //ninjectKernel.Rebind<UserConfig>().ToSelf()
            //.WithPropertyValue("ShowTab", false) //如果需要系统默认以多标签形式显示页，请设置为true
            //.WithPropertyValue("Theme", "blue");  //系统默认皮肤

            //Todo: 额外的注入代码
            ninjectKernel.Bind<IUserApplication>().To<UserApplication>();
            ninjectKernel.Bind<ISearchHistoryService>().To<SearchHistoryService>();
            ninjectKernel.Bind<IMetadataDefinitionService>().To<MetadataDefinitionService>();
            ninjectKernel.Bind<ISearchConfigService>().To<SearchConfigService>();

        }
    }
}
