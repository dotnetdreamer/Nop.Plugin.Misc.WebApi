using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using Nop.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Nop.Plugin.Misc.WebApi.WebApi
{
    public class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            //Enable CORS. Do not add move this to web.config. PUT and POST will not work.
            var attrs = new EnableCorsAttribute("http://localhost:8100", "*", "*"); //To be restricted properly in Production
            config.EnableCors(attrs);

            // Attribute routes
            config.MapHttpAttributeRoutes();
            //register regular routes  
            ApiRouteConfig.Register(config);

            config.SuppressDefaultHostAuthentication();

            //Autofac WebApi Container
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Update existing, don't create a new container
            builder.Update(EngineContext.Current.ContainerManager.Container);

            //Feed the current container to the AutofacWebApiDependencyResolver
            var resolver = new AutofacWebApiDependencyResolver(EngineContext.Current.ContainerManager.Container);
            config.DependencyResolver = resolver;

            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;

            //we will get JSON by default, but it will still allow you to return XML if you pass text/xml as the request Accept header
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
            //lower case the javascript properties
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.UseDataContractJsonSerializer = false;
        }

    }
}
