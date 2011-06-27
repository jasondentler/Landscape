using System;

namespace Restbucks.Barista
{

    public class when_serializing_OrderBeingPrepared
        : JsonEventSerializationFixture<OrderBeingPrepared>
    {
        protected override OrderBeingPrepared GivenEvent()
        {
            return new OrderBeingPrepared(Guid.NewGuid());
        }
    }
}
