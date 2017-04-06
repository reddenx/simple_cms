using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cms_ui.Models.Cms.Entities
{
    public class CmsContainer
    {
        public List<IContent> AllContent { get; set; }
        public List<ILocation> AllLocations { get; set; }
    }
}