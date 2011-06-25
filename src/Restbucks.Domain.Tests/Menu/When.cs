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
            var productId = Guid.NewGuid();
            var name = "Coffee";
            var price = 7.20M;

            DomainHelper.SetId<Product>(productId, name);

            var cmd = new AddProduct(productId, name, price);

            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I add coffee sizes")]
        public void WhenIAddCoffeeSizes()
        {
            var productId = DomainHelper.GetId<Product>("Coffee");
            var customization = "Size";
            var sizes = new[] {"Short", "Tall", "Grande", "Venti"};

            var cmd = new AddCustomization(productId, customization, sizes);

            DomainHelper.WhenExecuting(cmd);
        }
    
    }
}
