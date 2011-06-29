using System;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Saga.Mapping;

namespace Restbucks.Sagas
{
    public class DeliverySagaMapping : ISagaMapping 
    {
        public void RegisterMappings(InProcessEventBus eventBus)
        {
            Console.WriteLine("Mapping {0} to {1}", typeof(Billing.OrderPaid), typeof(DeliverySaga));
            Map.Event<Billing.OrderPaid>()
                .ToSaga<DeliverySaga>()
                .WithId(e => e.DeliverySagaId)
                .OrCreate(id => new DeliverySaga(id))
                .ToCallOn((e, saga) => saga.Paid(e))
                .RegisterWith(eventBus);

            Console.WriteLine("Mapping {0} to {1}", typeof(Barista.OrderPrepared), typeof(DeliverySaga));
            Map.Event<Barista.OrderPrepared>()
                .ToSaga<DeliverySaga>()
                .WithId(e => e.DeliverySagaId)
                .OrCreate(id => new DeliverySaga(id))
                .ToCallOn((e, saga) => saga.Prepared(e))
                .RegisterWith(eventBus);

        }
    }
}
