using cms_ui.Models.Cms;
using cms_ui.Models.Cms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cms_ui.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        private CmsContainer Container = DummyRepo.Get();

        public HomeController()
        { }

        [HttpPost]
        [Route("render/{locationId:guid}")]
        public ActionResult RenderContent(Guid locationId, Dictionary<string,string> inputs)
        {
            var location = Container.AllLocations.FirstOrDefault(l => l.Id == locationId);
            if (location == null || inputs == null)
            {
                return this.HttpNotFound();
            }

            var content = location.GetAppropriateContent(inputs);
            if(content == null)
            {
                return this.HttpNotFound();
            }

            var renderedData = content.GetRenderedContent(inputs);

            return this.Content(renderedData, "text/html", System.Text.Encoding.UTF8);
        }

        [HttpGet]
        [Route("")]
        public ViewResult Index()
        {
            return View();
        }
    }
}