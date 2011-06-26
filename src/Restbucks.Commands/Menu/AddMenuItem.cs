using System;
using Ncqrs.Commanding;

namespace Restbucks.Menu
{

    public class AddMenuItem : CommandBase 
    {
        public Guid MenuItemId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public AddMenuItem(
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
