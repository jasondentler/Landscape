using Example.Services;
using Ninject;
using Ninject.Modules;

namespace Example.Wiring
{
    public class ServicesModule : NinjectModule 
    {
        public override void Load()
        {
            SetupProductService();
        }

        private void SetupProductService()
        {
            // To share a single instance with the handler registration
            // If we used a db, we could separate the handler from the service
            Kernel.Bind<IProductService>()
                .ToMethod(ctx => ctx.Kernel.Get<ProductService>());

            Kernel.Bind<ServicesModule>()
                .ToSelf()
                .InSingletonScope();
        }

    }
}
