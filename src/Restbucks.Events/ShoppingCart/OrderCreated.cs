using System;

namespace Restbucks.ShoppingCart
{
    public class OrderCreated  : IEvent 
    {
        public Guid OrderId { get; private set; }

        public OrderCreated(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
