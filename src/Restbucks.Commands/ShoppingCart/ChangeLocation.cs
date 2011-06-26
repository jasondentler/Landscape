using System;
using Ncqrs.Commanding;

namespace Restbucks.ShoppingCart
{
    public class ChangeLocation : CommandBase 
    {
        public Guid CartId { get; private set; }
        public Location NewLocation { get; private set; }

        public ChangeLocation(
            Guid cartId,
            Location newLocation)
        {
            CartId = cartId;
            NewLocation = newLocation;
        }
    }
}
