using System;
using SharpTestsEx;
using TechTalk.SpecFlow;

namespace Restbucks.Billing
{
    [Binding]
    public class Then
    {

        [Then(@"coffee is added to the price list with a price of \$7\.20")]
        public void ThenCoffeeIsAddedToThePriceListWithAPriceOf7_20()
        {
            var menuItemId = DomainHelper.GetId<Menu.MenuItem>("Coffee");

            var e = DomainHelper.GetEvent<ProductAdded>();

            // We don't know what ID was generated, but we at least know two values it shouldn't be...
            e.ProductId.Should().Not.Be.EqualTo(Guid.Empty);
            e.ProductId.Should().Not.Be.EqualTo(menuItemId);

            e.MenuItemId.Should().Be.EqualTo(menuItemId);
            e.Name.Should().Be.EqualTo("Coffee");
            e.Price.Should().Be.EqualTo(7.20M);


        }
    
    }
}
