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
            var menuProductId = DomainHelper.GetId<Menu.Product>("Coffee");

            var e = DomainHelper.GetEvent<ProductAdded>();

            // We don't know what ID was generated, but we at least know two values it shouldn't be...
            e.ProductId.Should().Not.Be.EqualTo(Guid.Empty);
            e.ProductId.Should().Not.Be.EqualTo(menuProductId);

            e.MenuProductId.Should().Be.EqualTo(menuProductId);
            e.Name.Should().Be.EqualTo("Coffee");
            e.Price.Should().Be.EqualTo(7.20M);


        }
    
    }
}
