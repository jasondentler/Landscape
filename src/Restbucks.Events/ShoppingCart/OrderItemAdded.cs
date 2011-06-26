using System;
using System.Collections.Generic;

namespace Restbucks.ShoppingCart
{

    public class OrderItemAdded : IEvent
    {

        public Guid OrderId { get; private set; }
        public Guid OrderItemId { get; private set; }
        public Guid MenuItemId { get; private set; }
        public IDictionary<string, string> Preferences { get; private set; }
        public int Quantity { get; private set; }

        public OrderItemAdded(
            Guid orderId,
            Guid orderItemId,
            Guid menuItemId,
            IDictionary<string, string> preferences,
            int quantity)
        {
            OrderId = orderId;
            OrderItemId = orderItemId;
            MenuItemId = menuItemId;
            Preferences = preferences;
            Quantity = quantity;
        }
    }

}
