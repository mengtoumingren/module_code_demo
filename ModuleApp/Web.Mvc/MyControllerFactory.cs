using ModuleCore.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Mvc
{
    public class MyControllerFactory: DefaultControllerFactory
    {
        private IIocContainer iocContainer;
        public MyControllerFactory(IIocContainer iocContainer)
        {
            this.iocContainer = iocContainer;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                return iocContainer.GetInstance(controllerType) as IController;
            }
            catch (Exception)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }
            
        }
    }
}