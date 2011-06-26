using System;
using TechTalk.SpecFlow;

namespace Restbucks.Menu
{
    [Binding]
    public class When
    {
        [When(@"I add coffee to the menu with a price of \$7\.20")]
        public void WhenIAddCoffeeToTheMenuWithAPriceOf7_20()
        {
            var menuItemId = Guid.NewGuid();
            var name = "Coffee";
            var price = 7.20M;

            DomainHelper.SetId<MenuItem>(menuItemId, name);

            var cmd = new AddMenuItem(menuItemId, name, price);

            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I add coffee sizes")]
        public void WhenIAddCoffeeSizes()
        {
            var menuItemId = DomainHelper.GetId<MenuItem>("Coffee");
            var customization = "Size";
            var sizes = new[] {"Short", "Tall", "Grande", "Venti"};

            var cmd = new AddCustomization(menuItemId, customization, sizes);

            DomainHelper.WhenExecuting(cmd);
        }
    
    }
}
