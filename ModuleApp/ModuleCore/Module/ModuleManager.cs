using ModuleCore.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleCore.Module
{
    public class ModuleManager : IModuleManager
    {
        private static Dictionary<Type, int> moduleOrder = new Dictionary<Type, int>();
        private static int orderIndex = 0;
        public void Initialize(Type startupModule)
        {
            LoadModule(startupModule);
            moduleOrder = moduleOrder.OrderByDescending(item => item.Value).ToDictionary(item => item.Key, item => item.Value);
            foreach (var item in moduleOrder)
            {
                var module = Activator.CreateInstance(item.Key) as BaseModule;
                module.Container = IocContainer.Getinstance();
                module.Register();
            }
        }

        private void LoadModule(Type currentModule)
        {
            if(moduleOrder.ContainsKey(currentModule))
            {
                moduleOrder[currentModule] = orderIndex++;
            }
            else
            {
                moduleOrder.Add(currentModule, orderIndex++);
            }
            var depands = currentModule.GetCustomAttributes(typeof(DependOnAttribute), false);
            if (depands != null && depands.Count()>0)
            {
                foreach (var depand in depands)
                {
                    var attr = depand as DependOnAttribute;
                    foreach (var module in attr.modules)
                    {
                        LoadModule(module);
                    }
                    
                }
            }
        }

    }
}
