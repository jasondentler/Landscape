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

            AggregateRootHelper.SetIdFor<MenuItem>(menuItemId, name);

            var cmd = new AddMenuItem(menuItemId, name, price);

            WhenHelper.WhenExecuting(cmd);
        }

        [When(@"I add coffee sizes")]
        public void WhenIAddCoffeeSizes()
        {
            var menuItemId = AggregateRootHelper.GetIdFor<MenuItem>("Coffee");
            var customization = "Size";
            var sizes = new[] {"Short", "Tall", "Grande", "Venti"};

            var cmd = new AddCustomization(menuItemId, customization, sizes);

            WhenHelper.WhenExecuting(cmd);
        }
    
    }
}
