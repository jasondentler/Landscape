using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Storage;
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

            Kernel.Bind<IUniqueIdentifierGenerator>()
                .ToConstant(new BasicGuidGenerator());

            Kernel.Bind<ISagaCreationStrategy>()
                .To<SagaCreationStrategy>();

            SetupEventStorage();
            SetupCommandService();
            SetupEventBus();

        }

        private void SetupEventStorage()
        {
            const string connectionStringName = "EventStore";
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName];
            var store = new Ncqrs.Eventing.Storage.SQL.MsSqlServerEventStore(connectionString.ConnectionString);
            Kernel.Bind<IEventStore>()
                .ToConstant(store);
        }

        private void SetupCommandService()
        {
            var commandService = new CommandService();
            commandService.Configure();
            var queuedCommandService = new QueuedCommandService(commandService);
            Kernel.Bind<ICommandService>()
                .ToConstant(queuedCommandService);
        }

        private void SetupEventBus()
        {
            var eventBus = new InProcessEventBus();
            var asm = typeof(Restbucks.Billing.MenuItemAddedHandler).Assembly;
            eventBus.RegisterAllHandlersInAssembly(asm,
                                                   t => Kernel.Get(t));
            eventBus.RegisterSagaMappingsIn(typeof(DeliverySagaMapping).Assembly);

            var aggregateRootCreatedHandler = new EntityCreatedHandler();
            aggregateRootCreatedHandler.RegisterWith(eventBus);

            Kernel.Bind<IEventBus>()
                .ToConstant(eventBus);
        }

    }
}
