using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Saga.Domain.Billing;
using Ncqrs.Saga.Domain.Shipping;
using Ncqrs.Saga.Mapping;

namespace Ncqrs.Saga.Sagas
{
    public class ShippingSagaMapping : ISagaMapping 
    {
        public void RegisterMappings(InProcessEventBus eventBus)
        {

            Map.Event<InvoicePaid>()
                .ToSaga<ShippingSaga>()
                .WithId(e => e.ShippingSagaId)
                .OrCreate(id => new ShippingSaga(id))
                .ToCallOn((e, saga) => saga.InvoicePaid(e))
                .RegisterWith(eventBus);

            Map.Event<ShipmentPrepared>()
                .ToSaga<ShippingSaga>()
                .WithId(e => e.ShippingSagaId)
                .OrCreate(id => new ShippingSaga(id))
                .ToCallOn((e, saga) => saga.ShipmentPrepared(e))
                .RegisterWith(eventBus);

        }
    }
}
