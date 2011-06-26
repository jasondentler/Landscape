using System.Linq;
using SharpTestsEx;
using TechTalk.SpecFlow;

namespace Restbucks.Menu
{
    [Binding]
    public class Then
    {

        [Then(@"coffee is added to the menu with a price of \$7\.20")]
        public void ThenCoffeeIsAddedToTheMenuWithAPriceOf7_20()
        {
            var name = "Coffee";
            var productId = DomainHelper.GetId<Product>(name);

            var e = DomainHelper.GetEvent<ProductAdded>();
            e.ProductId.Should().Be.EqualTo(productId);
            e.Name.Should().Be.EqualTo(name);
            e.Price.Should().Be.EqualTo(7.20M);
        }

        [Then(@"coffee sizes are added")]
        public void ThenCoffeeSizesAreAdded()
        {
            var e = DomainHelper.GetEvent<CustomizationAdded>();
            e.Customization.Should().Be.EqualTo("Size");
            e.Options.Count().Should().Be.EqualTo(4);
            e.Options.Should().Contain("Short");
            e.Options.Should().Contain("Tall");
            e.Options.Should().Contain("Grande");
            e.Options.Should().Contain("Venti");
        }


    }
}
