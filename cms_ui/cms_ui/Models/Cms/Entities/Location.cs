using cms_ui.Models.Cms.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cms_ui.Models.Cms.Entities
{
    public interface ILocation
    {
        Guid Id { get; }

        /// <summary>
        /// unique name for this content location e.g. homepage_banner
        /// </summary>
        string Name { get; }

        /// <summary>
        /// the url to get a preview of this location for preview tools
        /// </summary>
        string PreviewLocationUrl { get; }

        /// <summary>
        /// all the associated content for this location
        /// </summary>
        ContentLocationRelationship[] AllAssociatedContent { get; }

        /// <summary>
        /// gets associated content that satisfy the constraints and can be rendered with the given input
        /// </summary>
        /// <param name="constraints"></param>
        /// <param name="inputs"></param>
        /// <returns>eligible content</returns>
        IContent GetAppropriateContent(Dictionary<string, string> input);

        /// <summary>
        /// updates or adds a content given the constraints 
        /// </summary>
        /// <param name="content"></param>
        void AssociateContent(IContent content, int priority, params InputMatchRequirement[] matchRequirements);

        /// <summary>
        /// removes the content location association
        /// </summary>
        /// <param name="content"></param>
        bool DisassociateContent(IContent content);
    }

    public class Location : ILocation
    {
        private List<ContentLocationRelationship> Relationships;

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string PreviewLocationUrl { get; private set; }

        public ContentLocationRelationship[] AllAssociatedContent { get { return Relationships.ToArray(); } }

        public Location(string name, string previewLocationUrl)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Relationships = new List<ContentLocationRelationship>();
            this.PreviewLocationUrl = previewLocationUrl;
        }

        public static ILocation Inflate(Guid id, string name, string previewUrl, ContentLocationRelationship[] relationships)
        {
            return new Location(id, name, previewUrl, relationships);
        }

        private Location(Guid id, string name, string previewUrl, ContentLocationRelationship[] relationships)
        {
            this.Id = id;
            this.Name = name;
            this.PreviewLocationUrl = previewUrl;
            this.Relationships = new List<ContentLocationRelationship>(relationships);
        }

        public void AssociateContent(IContent content, int priority, params InputMatchRequirement[] matchRequirements)
        {
            matchRequirements = matchRequirements ?? new InputMatchRequirement[] { };

            //remove any previous and add a new one
            Relationships.RemoveAll(r => r.Content.Id == content.Id);
            var relationship = new ContentLocationRelationship(content, this, matchRequirements, priority);
            Relationships.Add(relationship);
        }

        public bool DisassociateContent(IContent content)
        {
            return Relationships.RemoveAll(r => r.Content.Id == content.Id) > 0;
        }

        public IContent GetAppropriateContent(Dictionary<string, string> input)
        {
            //run matches against all relationships
            var eligibleRelationships = Relationships.Where(r => r.IsSatisfiedByInput(input));

            //return highest priority eligible relationship
            return eligibleRelationships.OrderByDescending(r => r.Priority).FirstOrDefault()?.Content;
        }
    }
}