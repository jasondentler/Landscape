using System;
using System.Collections.Generic;
using Ncqrs.Domain;

namespace Restbucks.Ordering
{
    public class OrderItem : Entity<Order>
    {

        public OrderItem(
            Order order,
            Guid orderItemId,
            Guid productId,
            IDictionary<string, string> preferences,
            int quantity)
            : base(order, orderItemId)
        {
            var e = new OrderItemAdded(
                order.EventSourceId,
                orderItemId,
                productId,
                preferences,
                quantity);
            ApplyEvent(e);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as OrderItem);
        }

        public bool Equals(OrderItem other)
        {
            if (null == other)
                return false;
            return other.EntityId == EntityId;
        }

        public override int GetHashCode()
        {
            return EntityId.GetHashCode();
        }
        
    }
}
