using System;

namespace Restbucks.Billing
{

    public class OrderPlaced : IEvent 
    {
        public Guid OrderId { get; private set; }
        public Guid ShoppingCardOrderId { get; private set; }
        public decimal OrderTotal { get; private set; }

        public OrderPlaced(
            Guid orderId,
            Guid shoppingCardOrderId,
            decimal orderTotal)
        {
            OrderId = orderId;
            ShoppingCardOrderId = shoppingCardOrderId;
            OrderTotal = orderTotal;
        }
    }

}
