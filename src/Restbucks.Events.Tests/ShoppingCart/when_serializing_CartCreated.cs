using System;

namespace Restbucks.ShoppingCart
{

    public class when_serializing_CartCreated
        : JsonEventSerializationFixture<CartCreated>
    {
        protected override CartCreated GivenEvent()
        {
            return new CartCreated(Guid.NewGuid());
        }
    }
}
