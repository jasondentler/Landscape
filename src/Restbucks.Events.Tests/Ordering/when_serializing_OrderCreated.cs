﻿using System;

namespace Restbucks.Ordering
{

    public class when_serializing_OrderCreated
        : JsonEventSerializationFixture<OrderCreated>
    {
        protected override OrderCreated GivenEvent()
        {
            return new OrderCreated(Guid.NewGuid());
        }
    }
}
