using System;
using System.Collections.Generic;
using Ncqrs.Spec;

namespace Restbucks.Ordering
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
