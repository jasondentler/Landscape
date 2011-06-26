using System;
using System.Collections.Generic;
using Ncqrs.Commanding;

namespace Restbucks.ShoppingCart
{

    public class AddItem : CommandBase 
    {

        public Guid CartId { get; private set; }
        public Guid ItemId { get; private set; }
        public Guid MenuItemId { get; private set; }
        public IDictionary<string, string> Preferences { get; private set; }
        public int Quantity { get; private set; }

        public AddItem(
            Guid cartId,
            Guid itemId, 
            Guid menuItemId,
            IDictionary<string, string> preferences,
            int quantity)
        {
            CartId = cartId;
            ItemId = itemId;
            MenuItemId = menuItemId;
            Preferences = preferences;
            Quantity = quantity;
        }
    }

}
