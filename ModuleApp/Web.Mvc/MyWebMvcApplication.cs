using ModuleCore;
using ModuleCore.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Mvc
{
    public class MyWebMvcApplication<StartModule>: MyWebApplication<StartModule> where StartModule:BaseModule
    {
        protected override void Application_Start()
        {
            base.Application_Start();
            ControllerBuilder.Current.SetControllerFactory(new MyControllerFactory(ModuleStarter.GetContainer()));
            AreaRegistration.RegisterAllAreas();
        }
    }
}
