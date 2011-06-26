using System;

namespace Restbucks.Billing
{

    public class when_serializing_OrderCreated
        : JsonEventSerializationFixture<OrderPlaced>
    {
        protected override OrderPlaced GivenEvent()
        {
            return new OrderPlaced(Guid.NewGuid(), Guid.NewGuid(), 29.7M);
        }
    }
}
