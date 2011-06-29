using System;

namespace Restbucks.Barista
{

    public class when_serializing_OrderPrepared
        : JsonEventSerializationFixture<OrderPrepared>
    {
        protected override OrderPrepared GivenEvent()
        {
            return new OrderPrepared(Guid.NewGuid(), Guid.NewGuid());
        }
    }
}
