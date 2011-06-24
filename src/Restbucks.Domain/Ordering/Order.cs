using System;
using Ncqrs.Domain;

namespace Restbucks.Ordering
{

    public class Order : AggregateRootMappedByConvention 
    {

        private Order()
        {
        }

        public Order(Guid orderId)
            : base(orderId)
        {
            var e = new OrderCreated(EventSourceId);
            ApplyEvent(e);
        }

        protected void On(OrderCreated e)
        {
        }

    }

}
