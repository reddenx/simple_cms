using cms_ui.Models.Cms;
using cms_ui.Models.Cms.Dtos;
using cms_ui.Models.Cms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace cms_ui.Api
{
    [RoutePrefix("api/cms")]
    public class ContentApiController : ApiController
    {
        private readonly CmsContainer Container;

        public ContentApiController()
        {
            Container = DummyRepo.Get();
        }

        [Route("content")]
        [HttpGet]
        public ContentDto[] GetAllContent()
        {
            var content = Container.AllContent.Select(GetContentDto).ToArray();
            return content;
        }

        [Route("content/{contentId:guid}")]
        [HttpGet]
        public ContentDto GetSpecificContent(Guid contentId)
        {
            var content = Container.AllContent.FirstOrDefault(c => c.Id == contentId);
            return GetContentDto(content);
        }

        [Route("location")]
        [HttpGet]
        public LocationDto[] GetAllLocations()
        {
            var locations = Container.AllLocations.Select(GetLocationDto).ToArray();
            return locations;
        }

        [Route("location/{locationId:guid}")]
        [HttpGet]
        public LocationDto GetSpecificLocation(Guid locationId)
        {
            var location = Container.AllLocations.FirstOrDefault(l => l.Id == locationId);
            return GetLocationDto(location);
        }

        [Route("location/{locationId:guid}/relationship")]
        [HttpGet]
        public LocationRelationshipDto[] GetLocationRelationships(Guid locationId)
        {
            var location = Container.AllLocations.FirstOrDefault(l => l.Id == locationId);
            if (location == null) return null;

            var relationships = location.AllAssociatedContent.Select(BuildFromRelationship).ToArray();
            return relationships;
        }










        private ContentDto GetContentDto(IContent content)
        {
            if (content == null) return null;

            return new ContentDto()
            {
                Id = content.Id,
                InputRequirements = content.InputRequirements,
                Name = content.Name,
                RawContent = content.RawContent
            };
        }

        private LocationDto GetLocationDto(ILocation location)
        {
            if (location == null) return null;

            return new LocationDto()
            {
                Id = location.Id,
                Name = location.Name,
                PreviewLocationUrl = location.PreviewLocationUrl
            };
        }

        private LocationRelationshipDto BuildFromRelationship(ContentLocationRelationship relationship)
        {
            return new LocationRelationshipDto()
            {
                ContentId = relationship.Content.Id,
                LocationId = relationship.Location.Id,
                Priority = relationship.Priority,
                Requirements = relationship.MatchRequirements.Select(r =>
                {
                    return new InputRequirementDto()
                    {
                        Key = r.MatchKey,
                        Value = r.MatchValue,
                        MatchType = r.MatchType.ToString()
                    };
                }).ToArray(),
            };
        }
    }
}