using System;

namespace Restbucks.Barista
{
    public class OrderPrepared : IEvent 
    {

        public Guid OrderId { get; private set; }
        public Guid DeliverySagaId { get; private set; }

        public OrderPrepared(
            Guid orderId,
            Guid deliverySagaId)
        {
            OrderId = orderId;
            DeliverySagaId = deliverySagaId;
        }
    }
}
