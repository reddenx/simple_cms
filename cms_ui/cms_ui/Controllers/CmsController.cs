using cms_ui.Models.Cms;
using cms_ui.Models.Cms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cms_ui.Controllers
{
    [RoutePrefix("cms")]
    public class CmsController : Controller
    {
        private CmsContainer Container = DummyRepo.Get();

        [Route("locations")]
        [HttpGet]
        public ViewResult LocationsView()
        {
            return View();
        }

        [Route("location/{locationId:guid}")]
        [HttpGet]
        public ViewResult Location(Guid locationId)
        {
            return View(locationId);
        }
    }
}