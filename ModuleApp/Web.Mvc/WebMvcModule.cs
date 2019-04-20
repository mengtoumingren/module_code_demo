using ModuleCore.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Web.Mvc
{
    [DependOn(typeof(WebModule))]
    public class WebMvcModule : BaseModule
    {
        public override void Register()
        {
            Container.Register(Assembly.GetExecutingAssembly());
        }
    }
}
