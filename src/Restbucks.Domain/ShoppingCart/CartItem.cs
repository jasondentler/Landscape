using System;
using System.Collections.Generic;
using Ncqrs.Domain;

namespace Restbucks.ShoppingCart
{

    public class CartItem : Entity<Cart>
    {
        public Cart Cart { get; set; }
        public Guid ItemId { get; set; }
        public Guid MenuItemId { get; set; }
        public IDictionary<string, string> Preferences { get; set; }
        public int Quantity { get; set; }

        internal CartItem(
            Cart cart,
            ItemAdded e)
            : this(cart, e.ItemId, e.MenuItemId, e.Preferences, e.Quantity)
        {
        }

        internal CartItem(
            Cart cart,
            Guid itemId,
            Guid menuItemId,
            IDictionary<string, string> preferences,
            int quantity)
            : base(cart, itemId)
        {
            this.Cart = cart;
            ItemId = itemId;
            MenuItemId = menuItemId;
            Preferences = preferences;
            Quantity = quantity;
        }
    }
}
