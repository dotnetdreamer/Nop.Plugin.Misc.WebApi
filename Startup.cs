using Microsoft.Owin;
using Nop.Plugin.Misc.WebApi.WebApi;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Nop.Plugin.Misc.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Configure(config);
            app.Map("/api", inner => inner.UseWebApi(config));
        }
    }
}
