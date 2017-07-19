using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Nop.Plugin.Misc.WebApi.WebApi
{
    public class ApiRouteConfig
    {
        private const string WebApiPrefix = "api";

        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("DefaultApiWithId", String.Format("{{controller}}/{{id}}"), new { id = RouteParameter.Optional }, new { id = @"\d+" });
            config.Routes.MapHttpRoute("DefaultApiWithAction", String.Format("{{controller}}/{{action}}"));
            config.Routes.MapHttpRoute("DefaultApiGet", String.Format("{{controller}}"), new { action = "Get" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            config.Routes.MapHttpRoute("DefaultApiPost", String.Format("{{controller}}"), new { action = "Post" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

        }
    }
}
