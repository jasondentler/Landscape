using System;
using Ncqrs.Commanding;

namespace Restbucks.Billing
{

    public class CreateOrder : CommandBase 
    {
        public Guid OrderId { get; private set; }
        public Guid CartId { get; private set; }
        public OrderItemInfo[] Items { get; private set; }

        public CreateOrder(
            Guid orderId, 
            Guid cartId,
            OrderItemInfo[] items)
        {
            OrderId = orderId;
            CartId = cartId;
            Items = items;
        }
    }

}
