using System;

namespace Restbucks.Menu
{
    public class CustomizationAdded : IEvent 
    {

        public Guid MenuItemId { get; private set; }
        public string Customization { get; private set; }
        public string[] Options { get; private set; }

        public CustomizationAdded(
            Guid menuItemId,
            string customization,
            string[] options)
        {
            MenuItemId = menuItemId;
            Customization = customization;
            Options = options;
        }
    }
}
