using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cms_ui.Models.Cms.Dtos
{
    public class LocationRelationshipDto
    {
        public Guid ContentId;
        public Guid LocationId;
        public int Priority;
        public InputRequirementDto[] Requirements;
    }

    public class InputRequirementDto
    {
        public string Key;
        public string Value;
        public string MatchType;
    }
}