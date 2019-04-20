using Logger;
using ModuleCore.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt
{
    [DependOn(typeof(LoggerModule))]
    public class EncryptModule : BaseModule
    {
        public override void Register()
        {
            Container.Register(Assembly.GetExecutingAssembly());
        }
    }
}
