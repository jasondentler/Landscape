using System;

namespace Restbucks.Billing
{

    public class when_serializing_OrderPaid
        : JsonEventSerializationFixture<OrderPaid>
    {
        protected override OrderPaid GivenEvent()
        {
            return new OrderPaid(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        }
    }
}
