using System;
using Restbucks.Billing;

namespace Restbucks.ShoppingCart
{
    public class OrderPlaced : IEvent
    {
        public Guid CartId { get; private set; }
        public Location Location { get; private set; }
        public OrderItemInfo[] Items { get; private set; }

        public OrderPlaced(
            Guid cartId,
            Location location,
            OrderItemInfo[] items)
        {
            CartId = cartId;
            Location = location;
            Items = items;
        }
    }
}
