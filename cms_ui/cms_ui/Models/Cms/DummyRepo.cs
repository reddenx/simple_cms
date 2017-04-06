using cms_ui.Models.Cms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cms_ui.Models.Cms
{
    public static class DummyRepo
    {
        private static CmsContainer TheThing;

        //private const string SessionKey = "stupid_keys_ugh";

        public static CmsContainer Get()
        {
            //if (HttpContext.Current.Session[SessionKey] is CmsContainer)
            //{
            //    return HttpContext.Current.Session[SessionKey] as CmsContainer;
            //}
            if(TheThing != null)
            {
                return TheThing;
            }

            var container = new CmsContainer();

            var homePage = new Location("homePage_banner", "http://localhost:37020/");
            var homePageAd1 = new Location("homepage_adspace_1", "http://localhost:37020/");

            var defaultBanner = new Content("default_banner", new string[] { }, @"<div> default banner </div>");
            var myNameBanner = new Content("banner_with_username", new string[] { "Username" }, @"<div> hi there @Username </div>");

            homePage.AssociateContent(myNameBanner, 10, new InputMatchRequirement("Username", MatchTypes.Exists));
            homePage.AssociateContent(defaultBanner, 0);

            container.AllContent = new List<IContent> { defaultBanner, myNameBanner };
            container.AllLocations = new List<ILocation> { homePage, homePageAd1 };

            //HttpContext.Current.Session[SessionKey] = container;
            TheThing = container;
            return container;
        }
    }
}