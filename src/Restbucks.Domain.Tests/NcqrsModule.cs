using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Saga;
using Ncqrs.Saga.Mapping;
using Ninject;
using Ninject.Modules;
using Restbucks.Sagas;

namespace Restbucks
{
    public class NcqrsModule : NinjectModule
    {

        public override void Load()
        {

            Kernel.Bind<IKernel>().ToConstant(Kernel);

            Kernel.Bind<IUnitOfWorkFactory>()
                .To<UnitOfWorkFactory>();

            var commandService = new CommandService();
            commandService.Configure();

            var queuedCommandService = new QueuedCommandService(commandService);

            Kernel.Bind<ICommandService>()
                .ToConstant(queuedCommandService);

            Kernel.Bind<IUniqueIdentifierGenerator>()
                .ToConstant(new BasicGuidGenerator());

            var eventBus = new InProcessEventBus();
            var asm = typeof (Restbucks.Billing.MenuItemAddedHandler).Assembly;
            eventBus.RegisterAllHandlersInAssembly(asm,
                                                   t => Kernel.Get(t));

            eventBus.RegisterSagaMappingsIn(typeof (DeliverySagaMapping).Assembly);

            var aggregateRootCreatedHandler = new EntityCreatedHandler();
            aggregateRootCreatedHandler.RegisterWith(eventBus);

            Kernel.Bind<IEventBus>()
                .ToConstant(eventBus);

            Kernel.Bind<ISagaCreationStrategy>()
                .To<SagaCreationStrategy>();

        }
    }
}
