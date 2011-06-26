using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ninject;
using Ninject.Modules;

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
            var asm = typeof (Restbucks.Billing.ProductHandler).Assembly;
            eventBus.RegisterAllHandlersInAssembly(asm,
                                                   t => Kernel.Get(t));
            Kernel.Bind<IEventBus>()
                .ToConstant(eventBus);



        }
    }
}
