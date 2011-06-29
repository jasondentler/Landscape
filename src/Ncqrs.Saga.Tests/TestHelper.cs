using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs.Commanding;
using Ncqrs.Domain;
using Ncqrs.Eventing;
using Ncqrs.Eventing.ServiceModel.Bus;
using TechTalk.SpecFlow;

namespace Ncqrs.Saga
{

    public class TestHelper
    {

        public static Guid GetId<T>()
        {
            return GetId(typeof (T).ToString());
        }

        public static Guid GetId(string key)
        {
            var ctx = ScenarioContext.Current;
            if (ctx.ContainsKey(key))
                return (Guid) ctx[key];
            var id = Guid.NewGuid();
            ctx[key] = id;
            return id;
        }

        public static void Given<TEvent>(TEvent @event)
        {
            var bus = NcqrsEnvironment.Get<IEventBus>();
            var e = new UncommittedEvent(
                Guid.NewGuid(),
                Guid.NewGuid(),
                0,
                0,
                DateTime.UtcNow,
                @event,
                null);
            bus.Publish(e);
        }

        public static void When<TEvent>(TEvent @event)
        {
            var factory = new UnitOfWorkFactory();
            using (var uow = factory.CreateUnitOfWork(Guid.NewGuid()))
            {
                uow.Accept();

                using (var ctx = new SagaDispatchContext())
                {
                    Given(@event);
                    Dispatches = ctx.Dispatches;
                }
            }
        }

        private static IEnumerable<ICommand> Dispatches
        {
            get
            {
                var ctx = ScenarioContext.Current;
                return (IEnumerable<ICommand>) ctx["dispatches"];
            }
            set
            {
                var ctx = ScenarioContext.Current;
                ctx["dispatches"] = value;
            }
        }

        public static TCommand Then<TCommand>()
        {
            return Dispatches.OfType<TCommand>().Single();
        }

        public static IEnumerable<ICommand> DispatchedCommands()
        {
            return Dispatches;
        }

    }

}
