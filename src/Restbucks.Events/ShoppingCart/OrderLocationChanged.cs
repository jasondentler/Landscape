using System;

namespace Restbucks.ShoppingCart
{
    public class OrderLocationChanged : IEvent
    {
        public Guid OrderId { get; private set; }
        public Location PreviousLocation { get; private set; }
        public Location Location { get; private set; }

        public OrderLocationChanged(
            Guid orderId,
            Location previousLocation,
            Location location)
        {
            OrderId = orderId;
            PreviousLocation = previousLocation;
            Location = location;
        }
    }
}
