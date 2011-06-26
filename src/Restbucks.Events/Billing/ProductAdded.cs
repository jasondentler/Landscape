using System;

namespace Restbucks.Billing
{
    public class ProductAdded : IEvent
    {
        public Guid ProductId { get; private set; }
        public Guid MenuProductId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public ProductAdded(
            Guid productId,
            Guid menuProductId,
            string name,
            decimal price)
        {
            ProductId = productId;
            MenuProductId = menuProductId;
            Name = name;
            Price = price;
        }
    }
}
