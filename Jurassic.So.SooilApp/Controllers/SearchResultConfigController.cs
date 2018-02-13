using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jurassic.So.Application.Metadata;
using Jurassic.WebFrame;

namespace Jurassic.So.SooilApp.Controllers
{
    [AllowAnonymous]
    public class SearchResultConfigController:BaseController
    {
        private IMetadataDefinitionService _metadataDefinitionService;
        public SearchResultConfigController(IMetadataDefinitionService metadataDefinitionService)
        {
            _metadataDefinitionService = metadataDefinitionService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllMetadatas()
        {
            var metadataDefines=_metadataDefinitionService.GetAllMetadatadefines();
            return Json(metadataDefines,JsonRequestBehavior.AllowGet);
        }

        public ActionResult SideFilterConfig()
        {
            return View();
        }
        public ActionResult SideFilterConfigEdit()
        {
            return View();
        }

        public ActionResult SideItemConfig()
        {
            return View();
        }
        public ActionResult SideItemConfigEdit()
        {
            return View();
        }
        public ActionResult SearchConditionConfig()
        {
            return View();
        }
    }
}