using SMT.Utilities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cms_ui.Framework
{
    public class ConfigurationSettings : ConfigurationBase
    {
        [AppSettings("jquery_cdn")]
        public readonly string JQueryCdnUrl;

        [AppSettings("vuejs_cdn")]
        public readonly string VueJsCdnUrl;
    }
}