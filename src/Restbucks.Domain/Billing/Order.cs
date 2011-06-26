using System;
using System.Linq;
using Ncqrs.Domain;

namespace Restbucks.Billing
{
    public class Order : AggregateRootMappedByConvention 
    {

        private Order()
        {
        }

        public Order(
            Guid orderId,
            Guid shoppingCardOrderId,
            OrderItemInfo[] items,
            IProductInfo[] products)
            : base(orderId)
        {

            var orderTotal = items
                .Select(i => new
                                 {
                                     i.Quantity,
                                     products.Single(p => p.MenuItemId == i.MenuItemId).Price
                                 })
                .Select(i => i.Quantity*i.Price)
                .Sum();

            var e = new OrderCreated(EventSourceId, shoppingCardOrderId, orderTotal);
            ApplyEvent(e);

        }

        protected void On(OrderCreated e)
        {
        }

    }
}
