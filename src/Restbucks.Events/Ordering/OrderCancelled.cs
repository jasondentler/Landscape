using System;

namespace Restbucks.Ordering
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
