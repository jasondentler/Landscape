using System;
using System.Collections.Generic;
using Ncqrs.Domain;

namespace Restbucks.Ordering
{

    public class Order : AggregateRootMappedByConvention 
    {

        private readonly HashSet<OrderItem> _items = new HashSet<OrderItem>();

        private Order()
        {
        }

        public Order(
            Guid orderId,
            Guid orderItemId, 
            Guid productId, 
            IDictionary<string, string> prefernces,
            int quantity)
            : base(orderId)
        {
            var e = new OrderCreated(EventSourceId);
            ApplyEvent(e);

            AddItem(orderItemId, productId, prefernces, quantity);
        }

        public void AddItem(
            Guid orderItemId, 
            Guid productId,
            IDictionary<string, string> preferences,
            int quantity)
        {
            _items.Add(new OrderItem(this, orderItemId, productId, preferences, quantity));
        }

        protected void On(OrderCreated e)
        {
        }

        protected void On(OrderItemAdded e)
        {
        }

        protected void On(System.Guid e)
        {
            throw new Exception("wtf!?!?");
        }

    }

}
