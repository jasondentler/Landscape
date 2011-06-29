using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Saga.Sagas;
using Ninject.Modules;

namespace Ncqrs.Saga
{
    public class SagaModule : NinjectModule 
    {
        public override void Load()
        {
            SetupCommandService();
            SetupEventBus();

            Kernel.Bind<ISagaCreationStrategy>()
                .To<SagaCreationStrategy>();
        }

        private void SetupCommandService()
        {
            Kernel.Bind<ICommandService>()
                .To<NullCommandService>();
        }

        private void SetupEventBus()
        {
            var bus = new InProcessEventBus();
            var mapping = new ShippingSagaMapping();
            mapping.RegisterMappings(bus);

            Kernel.Bind<IEventBus>()
                .ToConstant(bus);
        }

    }
}
