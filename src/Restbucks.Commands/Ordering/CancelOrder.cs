using System;
using Ncqrs.Commanding;

namespace Restbucks.Ordering
{
    public class CancelOrder : CommandBase 
    {
        public Guid OrderId { get; private set; }

        public CancelOrder(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
