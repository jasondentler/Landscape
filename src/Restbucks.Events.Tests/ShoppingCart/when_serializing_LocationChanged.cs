using System;

namespace Restbucks.ShoppingCart
{

    public class when_serializing_LocationChanged
        : JsonEventSerializationFixture<LocationChanged>
    {
        protected override LocationChanged GivenEvent()
        {
            return new LocationChanged(Guid.NewGuid(),
                                            Location.TakeAway,
                                            Location.TakeAway);
        }
    }
}
