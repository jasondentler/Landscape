using System;

namespace Restbucks.Billing
{

    public class when_serializing_OrderCreated
        : JsonEventSerializationFixture<OrderCreated>
    {
        protected override OrderCreated GivenEvent()
        {
            return new OrderCreated(Guid.NewGuid(), Guid.NewGuid(), 29.7M);
        }
    }
}
