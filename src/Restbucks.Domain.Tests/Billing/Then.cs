using System;
using Ncqrs;
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

        [Then(@"the product catalog has coffee")]
        public void ThenTheProductCatalogHasCoffee()
        {
            var e = DomainHelper.GetEvent<ProductAdded>();
            var menuItemId = e.MenuItemId;

            var productService = NcqrsEnvironment.Get<IProductService>();
            var productInfo = productService.GetProductInfoByMenuItemId(menuItemId);

            productInfo.ProductId.Should().Be.EqualTo(e.ProductId);
            productInfo.MenuItemId.Should().Be.EqualTo(menuItemId);
            productInfo.Name.Should().Be.EqualTo(e.Name);
            productInfo.Price.Should().Be.EqualTo(e.Price);

        }

        [Then(@"the order total is \$(\d+\.\d{2})")]
        public void ThenTheOrderTotalIs(string totalString)
        {
            var total = decimal.Parse(totalString);

            var shoppingCardOrderId = DomainHelper.GetId<ShoppingCart.Order>();

            var e = DomainHelper.GetEvent<OrderCreated>();

            // We don't know what orderId was generated, but we can check two values that it shouldn't be.
            e.OrderId.Should().Not.Be.EqualTo(Guid.Empty);
            e.OrderId.Should().Not.Be.EqualTo(shoppingCardOrderId);

            e.ShoppingCardOrderId.Should().Be.EqualTo(shoppingCardOrderId);
            e.OrderTotal.Should().Be.EqualTo(total);
        }
    
    }
}
