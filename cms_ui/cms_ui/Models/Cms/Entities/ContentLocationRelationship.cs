using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cms_ui.Models.Cms.Entities
{
    public class ContentLocationRelationship
    {
        public IContent Content { get; private set; }
        public ILocation Location { get; private set; }
        public InputMatchRequirement[] MatchRequirements { get; private set; }
        public int Priority { get; private set; }

        public ContentLocationRelationship(IContent content, ILocation location, InputMatchRequirement[] matchRequirements, int priority)
        {
            this.Content = content;
            this.Location = location;
            this.MatchRequirements = matchRequirements;
            this.Priority = priority;
        }

        public bool IsSatisfiedByInput(Dictionary<string, string> input)
        {
            //make sure all match requirements are satisfied by at least one input
            var allMatch = MatchRequirements.All(match => input.Any(i => match.IsMatch(i.Key, i.Value)));
            if (!allMatch) return false;

            //make sure all of the inputs required to render the content are here
            var satisfiesContentInputs = Content.InputRequirements.All(req => input.Keys.Contains(req));
            if (!satisfiesContentInputs) return false;

            return true;
        }
    }
}