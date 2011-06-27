using System;

namespace Restbucks.ShoppingCart
{

    public class when_serializing_CartAbandoned
        : JsonEventSerializationFixture<CartAbandoned>
    {
        protected override CartAbandoned GivenEvent()
        {
            return new CartAbandoned(Guid.NewGuid());
        }
    }
}
