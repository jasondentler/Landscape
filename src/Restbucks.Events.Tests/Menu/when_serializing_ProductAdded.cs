using System;

namespace Restbucks.Menu
{

    public class when_serializing_ProductAdded
        : JsonEventSerializationFixture<ProductAdded>
    {
        protected override ProductAdded GivenEvent()
        {
            return new ProductAdded(Guid.NewGuid(), "Coffee", 29.7M);
        }
    }
}
