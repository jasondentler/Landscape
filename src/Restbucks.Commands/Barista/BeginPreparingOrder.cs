using System;
using Ncqrs.Commanding;

namespace Restbucks.Barista
{
    public class BeginPreparingOrder : CommandBase 
    {
        public Guid OrderId { get; private set; }

        public BeginPreparingOrder(
            Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
