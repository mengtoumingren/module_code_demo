using ModuleCore;
using ModuleCore.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web
{
    public class MyWebApplication<StartModule> :System.Web.HttpApplication where StartModule:BaseModule
    {
        protected virtual void Application_Start()
        {
            ModuleStarter.Start<StartModule>();
            
        }
    }
}
