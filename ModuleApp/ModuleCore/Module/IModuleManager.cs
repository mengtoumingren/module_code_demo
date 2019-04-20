using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleCore.Module
{
    public interface IModuleManager
    {
        void Initialize(Type startupModule);
    }
}
