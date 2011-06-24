using System;
using System.Collections.Generic;
using Ncqrs.Eventing.Sourcing;

namespace Restbucks.Ordering
{

    public class OrderItemAdded : SourcedEntityEvent 
    {

        public Guid OrderId { get; private set; }
        public Guid OrderItemId { get; private set; }
        public Guid ProductId { get; private set; }
        public IDictionary<string, string> Preferences { get; private set; }
        public int Quantity { get; private set; }

        public OrderItemAdded(
            Guid orderId,
            Guid orderItemId,
            Guid productId,
            IDictionary<string, string> preferences,
            int quantity)
        {
            OrderId = orderId;
            OrderItemId = orderItemId;
            ProductId = productId;
            Preferences = preferences;
            Quantity = quantity;
        }
    }

}
