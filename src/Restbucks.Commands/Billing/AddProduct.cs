using System;
using Ncqrs.Commanding;

namespace Restbucks.Billing
{
    public class AddProduct : CommandBase 
    {
        public Guid ProductId { get; private set; }
        public Guid MenuItemId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public AddProduct(
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
