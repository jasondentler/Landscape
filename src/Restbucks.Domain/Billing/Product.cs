using System;
using Ncqrs.Domain;

namespace Restbucks.Billing
{
    public class Product : AggregateRootMappedByConvention 
    {

        private Product()
        {
        }

        public Product(
            Guid productId,
            Guid menuItemId, 
            string name,
            decimal price)
            : base(productId)
        {
            var e = new ProductAdded(productId, menuItemId, name, price);
            ApplyEvent(e);
        }

        protected void On(ProductAdded e)
        {
        }

    }
}
