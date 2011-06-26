using System;

namespace Restbucks.ShoppingCart
{
    public class OrderCancelled  : IEvent 
    {
        public Guid OrderId { get; private set; }

        public OrderCancelled(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
