using Nop.Core.Plugins;
using Nop.Services.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Routing;

namespace Nop.Plugin.Misc.WebApi.Plugin
{
    public class MiscWebservice : BasePlugin, IMiscPlugin
    {
        #region Methods

        public override void Install()
        {
            //write owin keys
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            config.AppSettings.Settings.Remove("owin:appStartup");
            config.AppSettings.Settings.Remove("owin:AutomaticAppStartup");
            config.AppSettings.Settings.Add("owin:appStartup", "Nop.Plugin.Misc.WebApi.Startup");
            config.AppSettings.Settings.Add("owin:AutomaticAppStartup", "true");
            config.Save();

            base.Install();
        }

        public override void Uninstall()
        {
            //remove owin keys
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            config.AppSettings.Settings.Remove("owin:appStartup");
            config.AppSettings.Settings.Remove("owin:AutomaticAppStartup");
            config.AppSettings.Settings.Add("owin:AutomaticAppStartup", "false");
            config.Save();

            base.Uninstall();
        }

        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "MiscWebServices";
            routeValues = new RouteValueDictionary() { { "Namespaces", "Nop.Plugin.Misc.WebApi.Controllers" }, { "area", null } };
        }

        #endregion
    }
}
