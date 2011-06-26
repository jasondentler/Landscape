using System;
using System.Collections.Generic;

namespace Restbucks.ShoppingCart
{

    public class when_serializing_OrderItemAdded
        : JsonEventSerializationFixture<OrderItemAdded>
    {
        protected override OrderItemAdded GivenEvent()
        {
            return new OrderItemAdded(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                                      new Dictionary<string, string>()
                                          {
                                              {"Milk", "skim"},
                                              {"Size", "medium"}
                                          },
                                      5);
        }

    }
}
