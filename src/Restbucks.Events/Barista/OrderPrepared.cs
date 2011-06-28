using System;

namespace Restbucks.Barista
{
    public class OrderPrepared : IEvent 
    {

        public Guid OrderId { get; private set; }

        public OrderPrepared(
            Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
