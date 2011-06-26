using System;

namespace Restbucks.ShoppingCart
{

    public class when_serializing_OrderLocationChanged
        : JsonEventSerializationFixture<OrderLocationChanged>
    {
        protected override OrderLocationChanged GivenEvent()
        {
            return new OrderLocationChanged(Guid.NewGuid(),
                                            Location.TakeAway,
                                            Location.TakeAway);
        }
    }
}
