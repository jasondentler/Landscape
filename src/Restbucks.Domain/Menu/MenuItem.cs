using System;
using System.Collections.Generic;
using Ncqrs.Domain;

namespace Restbucks.Menu
{
    public class MenuItem : AggregateRootMappedByConvention
    {

        private string _name;
        private readonly HashSet<string> _customizations = new HashSet<string>();

        private MenuItem()
        {
        }

        public MenuItem(Guid menuItemId, string name, decimal price)
            : base(menuItemId)
        {
            var e = new MenuItemAdded(EventSourceId, name, price);
            ApplyEvent(e);
        }

        public void AddCustomization(string customization, string[] options)
        {
            if (_customizations.Contains(customization))
                throw new InvalidAggregateStateException("{0} already has a {1} customization.", _name, customization);

            var e = new CustomizationAdded(EventSourceId, customization, options);
            ApplyEvent(e);
        }

        protected void On(MenuItemAdded e)
        {
            _name = e.Name;
        }

        protected void On(CustomizationAdded e)
        {
            _customizations.Add(e.Customization);
        }

    }
}
