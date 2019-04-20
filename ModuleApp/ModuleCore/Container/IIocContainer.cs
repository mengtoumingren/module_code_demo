using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModuleCore.Container
{
    public interface IIocContainer
    {
        void Register(Type toType);
        void Register<TTo>();
        void Register(Assembly assembly);
        object GetInstance(Type toType);
        TTo GetInstance<TTo>();
    }
}
