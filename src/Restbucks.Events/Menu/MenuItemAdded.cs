using System;

namespace Restbucks.Menu
{
    public class MenuItemAdded : IEvent
    {
        public Guid MenuItemId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public MenuItemAdded(
            Guid menuItemId,
            string name,
            decimal price)
        {
            MenuItemId = menuItemId;
            Name = name;
            Price = price;
        }
    }
}
