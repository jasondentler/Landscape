using System;

namespace Restbucks.ShoppingCart
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
