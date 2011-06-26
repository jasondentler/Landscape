using System;
using Ncqrs.Commanding;

namespace Restbucks.ShoppingCart
{
    public class PlaceOrder : CommandBase 
    {
        public Guid OrderId { get; private set; }
        public Location Location { get; private set; }

        public PlaceOrder(
            Guid orderId,
            Location location)
        {
            OrderId = orderId;
            Location = location;
        }
    }
}
