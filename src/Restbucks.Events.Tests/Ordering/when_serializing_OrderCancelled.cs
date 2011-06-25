using System;

namespace Restbucks.Ordering
{

    public class when_serializing_OrderCancelled
        : JsonEventSerializationFixture<OrderCancelled>
    {
        protected override OrderCancelled GivenEvent()
        {
            return new OrderCancelled(Guid.NewGuid());
        }
    }
}
