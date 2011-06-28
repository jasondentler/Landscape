using System;
using Ncqrs.Commanding;

namespace Restbucks.Barista
{

    public class FinishPreparingOrder : CommandBase 
    {
        public Guid OrderId { get; private set; }

        public FinishPreparingOrder(Guid orderId)
        {
            OrderId = orderId;
        }
    }

}
