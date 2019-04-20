using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MyContainer = InjectContainer.Container;

namespace ModuleCore.Container
{
    public class IocContainer : IIocContainer
    {
        private static IIocContainer instance;
        private static MyContainer container;
        public IocContainer() { }

        static IocContainer()
        {
            container = MyContainer.GetContainer();
            container.Register(typeof(IocContainer));
            instance = new IocContainer();
        }

        public static IIocContainer Getinstance()
        {
            return instance;
        }


        public object GetInstance(Type toType)
        {
            return container.GetInstance<object>(toType);
        }

        public TTo GetInstance<TTo>()
        {
            return container.GetInstance<TTo>();
        }

        public void Register(Type toType)
        {
            container.Register(toType);
        }

        public void Register<TTo>()
        {
            container.Register(typeof(TTo));
        }

        public void Register(Assembly assembly)
        {
            var types = assembly.GetTypes();//.Where(t=>t.GetInterface(typeof(IDependService).FullName)!=null);
            foreach (var type in types)
            {
                container.Register(type);
            }
        }
    }
}
