using System;
using Ncqrs.Commanding;

namespace Restbucks.Ordering
{
    public class ChangeOrderLocation : CommandBase 
    {
        public Guid OrderId { get; private set; }
        public Location NewLocation { get; private set; }

        public ChangeOrderLocation(
            Guid orderId,
            Location newLocation)
        {
            OrderId = orderId;
            NewLocation = newLocation;
        }
    }
}
