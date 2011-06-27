using System;
using Ncqrs.Domain;

namespace Restbucks.Barista
{
    public class Order : AggregateRootMappedByConvention 
    {

        private Order()
        {
        }

        public Order(
            Guid orderId,
            Location location,
            OrderItemInfo[] items)
            : base(orderId)
        {
            var e = new OrderQueued(
                orderId,
                location,
                items);
            ApplyEvent(e);
        }

        protected void On(OrderQueued e)
        {
        }

    }
}
