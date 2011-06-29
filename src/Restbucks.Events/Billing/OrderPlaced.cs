using System;

namespace Restbucks.Billing
{

    public class OrderPlaced : IEvent 
    {
        public Guid OrderId { get; private set; }
        public Guid ShoppingCartOrderId { get; private set; }
        public decimal OrderTotal { get; private set; }
        public Guid DeliverySagaId { get; private set; }

        public OrderPlaced(
            Guid orderId,
            Guid shoppingCartOrderId,
            decimal orderTotal,
            Guid deliverySagaId)
        {
            OrderId = orderId;
            ShoppingCartOrderId = shoppingCartOrderId;
            OrderTotal = orderTotal;
            DeliverySagaId = deliverySagaId;
        }
    }

}
