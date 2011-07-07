using Cqrs;
using Cqrs.Commanding;
using Cqrs.Domain;
using Cqrs.EventStore;
using Cqrs.EventStore.MsSql;
using Cqrs.Eventing;
using Example.Denormalizers;
using Example.Menu;
using Ninject.Modules;

namespace Example.Wiring
{
    public class CqrsModule : NinjectModule 
    {

        public override void Load()
        {
            Kernel.Bind<IRepository>()
                .To<Repository>()
                .InSingletonScope();

            SetupMsSqlEventStore();

            Kernel.Bind<IEventPublisher>()
                .To<NinjectEventPublisher>()
                .InSingletonScope();


            RegisterHandlers();
            SetupCommandSender();
        }

        private void RegisterHandlers()
        {
            new HandlerRegistration(Kernel)
                .RegisterHandlers(typeof (ItemCommandHandler).Assembly)
                .RegisterHandlers(typeof (ServicesModule).Assembly)
                .RegisterHandlers(typeof (MenuList).Assembly);
        }

        private void SetupCommandSender()
        {

            var commandSender = new NinjectCommandSender(Kernel);

            Kernel.Bind<ICommandSender>()
                .ToConstant(commandSender);
        }

        private void SetupMsSqlEventStore()
        {
            Kernel.Bind<IEventStore>()
                .To<MsSqlEventStore>()
                .InSingletonScope();

            Kernel.Bind<ITypeNameResolver>()
                .To<SimpleTypeNameResolver>();

            Kernel.Bind<ISerializer<Cqrs.EventStore.MsSql.EventDescriptor>>()
                .To<JsonSerializer>();

        }


    }
}
