using System;
using Ncqrs.Commanding;

namespace Restbucks.ShoppingCart
{
    public class PlaceOrder : CommandBase 
    {
        public Guid CartId { get; private set; }
        public Location Location { get; private set; }

        public PlaceOrder(
            Guid cartId,
            Location location)
        {
            CartId = cartId;
            Location = location;
        }
    }
}
