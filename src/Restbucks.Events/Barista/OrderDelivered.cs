using System;

namespace Restbucks.Barista
{

    public class OrderDelivered : IEvent
    {

        public Guid OrderId { get; private set; }

        public OrderDelivered(Guid orderId)
        {
            OrderId = orderId;
        }
    }

}
