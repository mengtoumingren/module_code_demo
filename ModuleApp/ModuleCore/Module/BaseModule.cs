using ModuleCore.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModuleCore.Module
{
    public abstract class BaseModule
    {
        public IIocContainer Container { get; internal set; }
        public abstract void Register();
    }
}
