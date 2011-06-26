using Ninject.Modules;
using Restbucks.Billing;

namespace Restbucks
{
    public class ServiceModule : NinjectModule 
    {
        public override void Load()
        {
            Kernel.Bind<IProductService>()
                .To<ProductService>();
        }
    }
}
