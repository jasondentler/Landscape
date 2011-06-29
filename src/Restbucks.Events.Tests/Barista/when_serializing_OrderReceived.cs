using System;
using System.Collections.Generic;

namespace Restbucks.Barista
{

    public class when_serializing_OrderReceived
        : JsonEventSerializationFixture<OrderQueued>
    {
        protected override OrderQueued GivenEvent()
        {
            return new OrderQueued(
                Guid.NewGuid(), Location.TakeAway,
                new[]
                    {
                        new OrderItemInfo(
                            Guid.NewGuid(),
                            new Dictionary<string, string>()
                                {
                                    {"Size", "medium"}
                                },
                            4)
                    },
                    Guid.NewGuid());
        }
    }
}
