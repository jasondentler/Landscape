using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;

namespace Example.UI
{
    public class NinjectControllerFactory : DefaultControllerFactory 
    {
        private readonly IKernel _kernel;

        public NinjectControllerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        protected override IController GetControllerInstance(
            RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController) _kernel.Get(controllerType);
        }

    }
}