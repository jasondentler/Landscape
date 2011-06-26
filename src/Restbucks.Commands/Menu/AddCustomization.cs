using System;
using Ncqrs.Commanding;

namespace Restbucks.Menu
{

    public class AddCustomization : CommandBase 
    {

        public Guid MenuItemId { get; private set; }
        public string Customization { get; private set; }
        public string[] Options { get; private set; }

        public AddCustomization(
            Guid menuItemId,
            string customization, 
            params string[] options)
        {
            MenuItemId = menuItemId;
            Customization = customization;
            Options = options;
        }
    }

}
