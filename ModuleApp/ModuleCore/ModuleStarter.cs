using ModuleCore.Container;
using ModuleCore.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleCore
{
    public class ModuleStarter
    {
       

        public static void Start<TStartModule>()where TStartModule:BaseModule
        {
            var manager = new ModuleManager();
            manager.Initialize(typeof(TStartModule));
        }

        public static IIocContainer GetContainer()
        {
            return IocContainer.Getinstance();
        }
    }
}
