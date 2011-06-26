using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs.Commanding;

namespace Restbucks.Billing
{

    public class CreateOrder : CommandBase 
    {
        public Guid OrderId { get; private set; }
        public Guid ShoppingCardOrderId { get; private set; }
        public OrderItemInfo[] Items { get; private set; }

        public CreateOrder(
            Guid orderId, 
            Guid shoppingCardOrderId,
            OrderItemInfo[] items)
        {
            OrderId = orderId;
            ShoppingCardOrderId = shoppingCardOrderId;
            Items = items;
        }
    }

}
