using System;
using Ncqrs.Commanding;

namespace Restbucks.Menu
{

    public class AddProduct : CommandBase 
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public AddProduct(
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
