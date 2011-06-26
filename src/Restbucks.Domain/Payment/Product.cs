using System;
using Ncqrs.Domain;

namespace Restbucks.Payment
{
    public class Product : AggregateRootMappedByConvention 
    {

        private Product()
        {
        }

        public Product(
            Guid productId,
            Guid menuProductId, 
            string name,
            decimal price)
            : base(productId)
        {
            var e = new ProductAdded(productId, menuProductId, name, price);
            ApplyEvent(e);
        }

        protected void On(ProductAdded e)
        {
        }

    }
}
