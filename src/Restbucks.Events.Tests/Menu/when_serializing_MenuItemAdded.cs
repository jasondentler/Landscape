using System;

namespace Restbucks.Menu
{

    public class when_serializing_MenuItemAdded
        : JsonEventSerializationFixture<MenuItemAdded>
    {
        protected override MenuItemAdded GivenEvent()
        {
            return new MenuItemAdded(Guid.NewGuid(), "Coffee", 29.7M);
        }
    }
}
