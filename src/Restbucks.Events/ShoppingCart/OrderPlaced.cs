using System;

namespace Restbucks.ShoppingCart
{
    public class OrderPlaced : IEvent
    {
        public Guid OrderId { get; private set; }
        public Location Location { get; private set; }
        public OrderItemInfo[] Items { get; private set; }

        public OrderPlaced(
            Guid orderId,
            Location location,
            OrderItemInfo[] items)
        {
            OrderId = orderId;
            Location = location;
            Items = items;
        }
    }
}
