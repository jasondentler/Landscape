﻿using System;

namespace Restbucks.Billing
{
    public class ProductAdded : IEvent
    {
        public Guid ProductId { get; private set; }
        public Guid MenuItemId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public ProductAdded(
            Guid productId,
            Guid menuItemId,
            string name,
            decimal price)
        {
            ProductId = productId;
            MenuItemId = menuItemId;
            Name = name;
            Price = price;
        }
    }
}
