using System;
using Ncqrs.Commanding;

namespace Restbucks.Barista
{
    public class DeliverOrder : CommandBase
    {
        public Guid OrderId { get; private set; }

        public DeliverOrder(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
