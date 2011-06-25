using System;

namespace Restbucks.Menu
{
    public class ProductAdded 
    {
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public ProductAdded(
            Guid productId,
            string name,
            decimal price)
        {
            ProductId = productId;
            Name = name;
            Price = price;
        }
    }
}
