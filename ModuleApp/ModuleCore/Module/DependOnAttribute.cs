using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleCore.Module
{
    public class DependOnAttribute: Attribute
    {
        public Type[] modules { get; private set; }
        public DependOnAttribute(params Type[] module)
        {
            this.modules = module;
        }
    }
}
