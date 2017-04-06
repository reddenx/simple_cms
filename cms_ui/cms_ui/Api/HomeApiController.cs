using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace cms_ui.Api
{
    [RoutePrefix("json")]
    public class HomeApiController : ApiController
    {
        public HomeApiController()
        { }

        [HttpGet]
        [Route("version")]
        public string[] Version()
        {
            var assembly = Assembly.GetAssembly(GetType());
            var referencedAssemblies = assembly.GetReferencedAssemblies();
            var versions = referencedAssemblies
                .Concat(new[] { assembly.GetName() })
                .OrderBy(a => a.Name)
                .Select(a => $"{a.Name}: {a.Version}");

            return versions.ToArray();
        }
    }
}