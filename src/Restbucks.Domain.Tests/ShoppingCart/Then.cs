using System;
using System.Linq;
using Restbucks.Menu;
using SharpTestsEx;
using TechTalk.SpecFlow;

namespace Restbucks.ShoppingCart
{
    [Binding]
    public class Then
    {

        [Then(@"the cart is created")]
        public void ThenTheCartIsCreated()
        {
            var cartId = AggregateRootHelper.GetIdFor<Cart>();

            var e = ThenHelper.GetEvent<CartCreated>();
            e.CartId.Should().Be.EqualTo(cartId);
        }

        [Then(@"a medium cappuccino, skim milk, single shot is added to the cart")]
        public void ThenAMediumCappuccinoSkimMilkSingleShotIsAddedToTheCart()
        {
            var cartId = AggregateRootHelper.GetIdFor<Cart>();
            var itemId = AggregateRootHelper.GetIdFor<CartItem>();
            var menuItemId = AggregateRootHelper.GetIdFor<MenuItem>("Cappuccino");

            var e = ThenHelper.GetEvent<ItemAdded>();

            e.CartId.Should().Be.EqualTo(cartId);
            e.ItemId.Should().Be.EqualTo(itemId);
            e.MenuItemId.Should().Be.EqualTo(menuItemId);
            e.Preferences["Size"].Should().Be.EqualTo("medium");
            e.Preferences["Milk"].Should().Be.EqualTo("skim");
            e.Preferences["Shots"].Should().Be.EqualTo("single");
            e.Quantity.Should().Be.EqualTo(1);

        }

        [Then(@"a large hot chocolate, skim milk, no whipped cream is added to the cart")]
        public void ThenALargeHotChocolateSkimMilkNoWhippedCreamIsAddedToTheCart()
        {
            var cartId = AggregateRootHelper.GetIdFor<Cart>();
            var itemId = AggregateRootHelper.GetIdFor<CartItem>();
            var menuItemId = AggregateRootHelper.GetIdFor<MenuItem>("Hot Chocolate");

            var e = ThenHelper.GetEvent<ItemAdded>();

            e.CartId.Should().Be.EqualTo(cartId);
            e.ItemId.Should().Be.EqualTo(itemId);
            e.MenuItemId.Should().Be.EqualTo(menuItemId);
            e.Preferences["Size"].Should().Be.EqualTo("large");
            e.Preferences["Milk"].Should().Be.EqualTo("skim");
            e.Preferences["Whipped Cream"].Should().Be.EqualTo("no");
            e.Quantity.Should().Be.EqualTo(1);
        }

        [Then(@"the order is placed for take away")]
        public void ThenTheOrderIsPlacedForTakeAway()
        {
            var cartId = AggregateRootHelper.GetIdFor<Cart>();

            var e = ThenHelper.GetEvent<OrderPlaced>();
            e.CartId.Should().Be.EqualTo(cartId);
            e.Location.Should().Be.EqualTo(Location.TakeAway);

            e.DeliverySagaId.Should().Not.Be.EqualTo(Guid.Empty);
            e.DeliverySagaId.Should().Not.Be.EqualTo(cartId);
        }

        [Then(@"the placed order has one item")]
        public void ThenThePlacedOrderHasOneItem()
        {
            var e = ThenHelper.GetEvent<OrderPlaced>();
            e.Items.Length.Should().Be.EqualTo(1);
        }

        [Then(@"the placed order contains a medium cappuccino, skim milk, single shot")]
        public void ThenThePlacedOrderContainsAMediumCappuccinoSkimMilkSingleShot()
        {
            var cappucinoMenuItemId = AggregateRootHelper.GetIdFor<MenuItem>("Cappuccino");

            var e = ThenHelper.GetEvent<OrderPlaced>();

            var matchingItems = e.Items
                .Where(i => i.MenuItemId == cappucinoMenuItemId)
                .Where(i => i.Preferences["Size"] == "medium")
                .Where(i => i.Preferences["Milk"] == "skim")
                .Where(i => i.Preferences["Shots"] == "single")
                .Where(i => i.Quantity == 1);

            matchingItems.Any().Should().Be.True();

        }

        [Then(@"the order location is changed from in shop to take away")]
        public void ThenTheOrderLocationIsChangedFromInShopToTakeAway()
        {
            var cartId = AggregateRootHelper.GetIdFor<Cart>();

            var e = ThenHelper.GetEvent<LocationChanged>();

            e.CartId.Should().Be.EqualTo(cartId);
            e.PreviousLocation.Should().Be.EqualTo(Location.InShop);
            e.Location.Should().Be.EqualTo(Location.TakeAway);
        }

        [Then(@"the cart is abandoned")]
        public void ThenTheCartIsAbandoned()
        {
            var cartId = AggregateRootHelper.GetIdFor<Cart>();

            var e = ThenHelper.GetEvent<CartAbandoned>();

            e.CartId.Should().Be.EqualTo(cartId);
        }


    }
}
