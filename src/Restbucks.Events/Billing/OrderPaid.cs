using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restbucks.Billing
{

    public class OrderPaid : IEvent
    {

        public Guid OrderId { get; private set; }
        public Guid ShoppingCardOrderId { get; private set; }

        public OrderPaid(
            Guid orderId,
            Guid shoppingCardOrderId)
        {
            OrderId = orderId;
            ShoppingCardOrderId = shoppingCardOrderId;
        }
    }

}
