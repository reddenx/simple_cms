using cms_ui.Models.Cms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cms_ui.Models.Cms.Dtos
{
    public class ContentDto
    {
        public Guid Id;
        public string[] InputRequirements;
        public string Name;
        public string RawContent;
    }
}