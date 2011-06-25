using System;

namespace Restbucks.Ordering
{
    public class OrderPlaced : IEvent
    {
        public Guid OrderId { get; private set; }
        public Location Location { get; private set; }

        public OrderPlaced(
            Guid orderId,
            Location location)
        {
            OrderId = orderId;
            Location = location;
        }
    }
}
