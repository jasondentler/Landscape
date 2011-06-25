using Restbucks.Menu;
using SharpTestsEx;
using TechTalk.SpecFlow;

namespace Restbucks.Ordering
{
    [Binding]
    public class Then
    {

        [Then(@"the order is created")]
        public void ThenTheOrderIsCreated()
        {
            var orderId = DomainHelper.GetId<Order>();

            var e = DomainHelper.GetEvent<OrderCreated>();
            e.OrderId.Should().Be.EqualTo(orderId);
        }

        [Then(@"a medium cappuccino, skim milk, single shot is added to the order")]
        public void ThenAMediumCappuccinoSkimMilkSingleShotIsAddedToTheOrder()
        {
            var orderId = DomainHelper.GetId<Order>();
            var orderItemId = DomainHelper.GetId<OrderItem>();
            var productId = DomainHelper.GetId<Product>("Cappuccino");

            var e = DomainHelper.GetEvent<OrderItemAdded>();

            e.OrderId.Should().Be.EqualTo(orderId);
            e.OrderItemId.Should().Be.EqualTo(orderItemId);
            e.ProductId.Should().Be.EqualTo(productId);
            e.Preferences["Size"].Should().Be.EqualTo("medium");
            e.Preferences["Milk"].Should().Be.EqualTo("skim");
            e.Preferences["Shots"].Should().Be.EqualTo("single");
            e.Quantity.Should().Be.EqualTo(1);

        }

        [Then(@"a large hot chocolate, skim milk, no whipped cream is added to the order")]
        public void ThenALargeHotChocolateSkimMilkNoWhippedCreamIsAddedToTheOrder()
        {
            var orderId = DomainHelper.GetId<Order>();
            var orderItemId = DomainHelper.GetId<OrderItem>();
            var productId = DomainHelper.GetId<Product>("Hot Chocolate");

            var e = DomainHelper.GetEvent<OrderItemAdded>();

            e.OrderId.Should().Be.EqualTo(orderId);
            e.OrderItemId.Should().Be.EqualTo(orderItemId);
            e.ProductId.Should().Be.EqualTo(productId);
            e.Preferences["Size"].Should().Be.EqualTo("large");
            e.Preferences["Milk"].Should().Be.EqualTo("skim");
            e.Preferences["Whipped Cream"].Should().Be.EqualTo("no");
            e.Quantity.Should().Be.EqualTo(1);
        }

        [Then(@"the order is placed for take away")]
        public void ThenTheOrderIsPlacedForTakeAway()
        {
            var orderId = DomainHelper.GetId<Order>();

            var e = DomainHelper.GetEvent<OrderPlaced>();
            e.OrderId.Should().Be.EqualTo(orderId);
            e.Location.Should().Be.EqualTo(Location.TakeAway);
        }

        [Then(@"the order location is changed from in shop to take away")]
        public void ThenTheOrderLocationIsChangedFromInShopToTakeAway()
        {
            var orderId = DomainHelper.GetId<Order>();

            var e = DomainHelper.GetEvent<OrderLocationChanged>();

            e.OrderId.Should().Be.EqualTo(orderId);
            e.PreviousLocation.Should().Be.EqualTo(Location.InShop);
            e.Location.Should().Be.EqualTo(Location.TakeAway);
        }

        [Then(@"the order is cancelled")]
        public void ThenTheOrderIsCancelled()
        {
            var orderId = DomainHelper.GetId<Order>();

            var e = DomainHelper.GetEvent<OrderCancelled>();

            e.OrderId.Should().Be.EqualTo(orderId);
        }

    }
}
