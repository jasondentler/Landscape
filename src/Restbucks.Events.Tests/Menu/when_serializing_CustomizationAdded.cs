using System;
using Ncqrs.Spec;

namespace Restbucks.Menu
{

    public class when_serializing_CustomizationAdded
        : JsonEventSerializationFixture<CustomizationAdded>
    {

        protected override CustomizationAdded GivenEvent()
        {
            return new CustomizationAdded(Guid.NewGuid(), "Size", new string[] {"Tiny", "Big Gulp!"});
        }

    }
}
