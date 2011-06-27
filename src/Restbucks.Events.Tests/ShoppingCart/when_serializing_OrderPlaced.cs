using System;
using System.Collections.Generic;
using Restbucks.Billing;

namespace Restbucks.ShoppingCart
{

    public class when_serializing_OrderPlaced
        : JsonEventSerializationFixture<OrderPlaced>
    {
        protected override OrderPlaced GivenEvent()
        {
            return new OrderPlaced(
                Guid.NewGuid(), Location.TakeAway,
                new[]
                    {
                        new OrderItemInfo(
                            Guid.NewGuid(), Guid.NewGuid(),
                            new Dictionary<string, string>()
                                {
                                    {"Size", "medium"}
                                }, 4),
                    });
        }
    }
}
