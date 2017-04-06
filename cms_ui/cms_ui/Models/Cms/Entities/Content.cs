using cms_ui.Models.Cms.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cms_ui.Models.Cms.Entities
{
    public interface IContent
    {
        Guid Id { get; }

        /// <summary>
        /// unique name of this content e.g. december_promo_tiny
        /// </summary>
        string Name { get; }

        /// <summary>
        /// list of inputs used by this content segment, e.g. account_number
        /// </summary>
        string[] InputRequirements { get; }

        /// <summary>
        /// the html content string that will get rendered into the space on the page
        /// </summary>
        string RawContent { get; }

        /// <summary>
        /// All locations that have an association to this content
        /// </summary>
        ILocation[] AllAssociatedLocations { get; }

        /// <summary>
        /// sets the content
        /// </summary>
        /// <param name="content"></param>
        void SetContent(string rawContent);

        /// <summary>
        /// renders the content with the given inputs
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        string GetRenderedContent(Dictionary<string, string> inputs);
    }

    public class Content : IContent
    {
        private List<ContentLocationRelationship> Relationships;

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string[] InputRequirements { get; private set; }
        public string RawContent { get; private set; }

        public ILocation[] AllAssociatedLocations
        {
            get
            {
                return Relationships.Select(r => r.Location).ToArray();
            }
        }

        public Content(string name, string[] inputRequirements, string content)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.InputRequirements = inputRequirements;
            this.RawContent = content;

            this.Relationships = new List<ContentLocationRelationship>();
        }

        private Content(Guid id, string name, string[] inputRequirements, string content, ContentLocationRelationship[] relationships)
        {
            this.Id = id;
            this.Name = name;
            this.InputRequirements = inputRequirements;
            this.RawContent = RawContent;
            this.Relationships = new List<ContentLocationRelationship>(relationships);
        }

        public void SetContent(string rawContent)
        {
            this.RawContent = rawContent;
        }

        public string GetRenderedContent(Dictionary<string, string> inputs)
        {
            //if all requirements are present in the dictionary
            if (InputRequirements.All(requirement => inputs.Keys.Contains(requirement)))
            {
                var currentRenderedString = RawContent;
                foreach(var kvp in inputs)
                {
                    currentRenderedString = currentRenderedString.Replace($"@{kvp.Key}", kvp.Value);
                }
                return currentRenderedString;
                //throw new NotImplementedException("this is where mvc would render the view and give back content");
            }
            else
            {
                throw new ArgumentException("inputs does not contain the necessary inputs for this content");
            }
        }

        public ContentDto Deflate()
        {
            throw new NotImplementedException();
        }
    }
}