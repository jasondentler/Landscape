using System;

namespace Restbucks.Barista
{

    public class when_serializing_OrderDelivered
        : JsonEventSerializationFixture<OrderDelivered>
    {
        protected override OrderDelivered GivenEvent()
        {
            return new OrderDelivered(Guid.NewGuid());
        }
    }
}
