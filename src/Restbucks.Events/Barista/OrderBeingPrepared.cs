using System;

namespace Restbucks.Barista
{
    public class OrderBeingPrepared : IEvent 
    {

        public Guid OrderId { get; private set; }

        public OrderBeingPrepared(
            Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
