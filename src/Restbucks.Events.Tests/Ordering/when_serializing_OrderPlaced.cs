using System;

namespace Restbucks.Ordering
{

    public class when_serializing_OrderPlaced
        : JsonEventSerializationFixture<OrderPlaced>
    {
        protected override OrderPlaced GivenEvent()
        {
            return new OrderPlaced(Guid.NewGuid(), Location.TakeAway);
        }
    }
}
