using System;

namespace Restbucks.ShoppingCart
{
    public class LocationChanged : IEvent
    {
        public Guid CartId { get; private set; }
        public Location PreviousLocation { get; private set; }
        public Location Location { get; private set; }

        public LocationChanged(
            Guid cartId,
            Location previousLocation,
            Location location)
        {
            CartId = cartId;
            PreviousLocation = previousLocation;
            Location = location;
        }
    }
}
