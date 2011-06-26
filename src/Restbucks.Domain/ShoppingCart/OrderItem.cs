using System;
using System.Collections.Generic;
using Ncqrs.Domain;

namespace Restbucks.ShoppingCart
{

    /// <summary>
    /// Eventually, this wil be an entity. For now, it's just used to track order item Ids in the specs
    /// </summary>
    public class OrderItem
    {
        public Order Order { get; set; }
        public Guid OrderItemId { get; set; }
        public Guid MenuItemId { get; set; }
        public IDictionary<string, string> Preferences { get; set; }
        public int Quantity { get; set; }

        internal OrderItem(
            Order order,
            OrderItemAdded e)
            : this(order, e.OrderItemId, e.MenuItemId, e.Preferences, e.Quantity)
        {
        }

        internal OrderItem(
            Order order,
            Guid orderItemId,
            Guid menuItemId,
            IDictionary<string, string> preferences,
            int quantity)
        {
            Order = order;
            OrderItemId = orderItemId;
            MenuItemId = menuItemId;
            Preferences = preferences;
            Quantity = quantity;
        }
    }
}
