using Encrypt;
using Logger;
using ModuleCore.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Web.Mvc;

namespace WebApplication1.App_Start
{
    [DependOn(typeof(WebMvcModule),typeof(EncryptModule),typeof(LoggerModule))]
    public class ApplicationModule : BaseModule
    {
        public override void Register()
        {
            var assembly = Assembly.GetExecutingAssembly();
            Container.Register(assembly);
            //注册控制器
            var controllerTypes = assembly.GetTypes().Where(t => t.Name.EndsWith("Controller"));
            foreach (var item in controllerTypes)
            {
                Container.Register(item);
            }
        }
    }
}