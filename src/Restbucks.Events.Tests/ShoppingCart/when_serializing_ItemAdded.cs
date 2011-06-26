using System;
using System.Collections.Generic;

namespace Restbucks.ShoppingCart
{

    public class when_serializing_ItemAdded
        : JsonEventSerializationFixture<ItemAdded>
    {
        protected override ItemAdded GivenEvent()
        {
            return new ItemAdded(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                                      new Dictionary<string, string>()
                                          {
                                              {"Milk", "skim"},
                                              {"Size", "medium"}
                                          },
                                      5);
        }

    }
}
