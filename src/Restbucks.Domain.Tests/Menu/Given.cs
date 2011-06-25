using System;
using TechTalk.SpecFlow;

namespace Restbucks.Menu
{
    [Binding]
    public class Given
    {

        [Given(@"I have added coffee to the menu")]
        public void GivenIHaveAddedCoffeeToTheMenu()
        {
            var productId = Guid.NewGuid();
            var name = "Coffee";
            var price = 7.20M;

            DomainHelper.SetId<Product>(productId, name);
            
            var e = new ProductAdded(productId, name, price);
            DomainHelper.GivenEvent<Product>(e);
        }

        [Given(@"I have added coffee sizes")]
        public void GivenIHaveAddedCoffeeSizes()
        {
            var name = "Coffee";
            var productId = DomainHelper.GetId<Product>(name);

            var e = new CustomizationAdded(productId, "Size", new[] {"Short", "Tall", "Grande", "Venti"});
            DomainHelper.GivenEvent<Product>(e);
        }





    }
}
