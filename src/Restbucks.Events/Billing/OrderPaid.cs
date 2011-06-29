using System;

namespace Restbucks.Billing
{

    public class OrderPaid : IEvent
    {

        public Guid OrderId { get; private set; }
        public Guid ShoppingCardOrderId { get; private set; }
        public Guid DeliverySagaId { get; private set; }

        public OrderPaid(
            Guid orderId,
            Guid shoppingCardOrderId,
            Guid deliverySagaId)
        {
            OrderId = orderId;
            ShoppingCardOrderId = shoppingCardOrderId;
            DeliverySagaId = deliverySagaId;
        }
    }

}
