using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.WebApi.Controllers
{
    public class MiscWebServicesController : Controller
    {
        public ActionResult Configure()
        {
            return Content("Worked");
        }
    }
}
