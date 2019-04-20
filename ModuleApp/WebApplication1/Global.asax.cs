using ModuleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web.Mvc;
using WebApplication1.App_Start;

namespace WebApplication1
{
    public class MvcApplication : MyWebMvcApplication<ApplicationModule>
    {
        protected override void Application_Start()
        {
            base.Application_Start();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
