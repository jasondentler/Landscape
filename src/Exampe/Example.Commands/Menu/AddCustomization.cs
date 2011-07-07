using System;
using Cqrs.Commanding;

namespace Example.Menu
{
    public class AddCustomization : Command
    {

        public Guid MenuItemId { get; private set; }
        public string Name { get; private set; }
        public string[] Options { get; private set; }

        public AddCustomization(
            Guid menuItemId,
            string name,
            string[] options)
        {
            MenuItemId = menuItemId;
            Name = name;
            Options = options;
        }
    }
}
