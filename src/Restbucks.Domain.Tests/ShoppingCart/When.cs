using System;
using System.Collections.Generic;
using Restbucks.Menu;
using TechTalk.SpecFlow;

namespace Restbucks.ShoppingCart
{
    [Binding]
    public class When
    {

        private Guid GetCartId()
        {
            if (AggregateRootHelper.IdExists<Cart>())
                return AggregateRootHelper.GetIdFor<Cart>();
            var cartId = Guid.NewGuid();
            AggregateRootHelper.SetIdFor<Cart>(cartId);
            return cartId;
        }

        [When(@"I add a medium cappuccino, skim milk, single shot")]
        public void WhenIAddAMediumCappuccinoSkimMilkSingleShot()
        {

            var cartId = GetCartId();
            var itemId = Guid.NewGuid();
            var menuItemId = AggregateRootHelper.GetIdFor<MenuItem>("Cappuccino");
            var preferences = new Dictionary<string, string>()
                                  {
                                      {"Size", "medium"},
                                      {"Milk", "skim"},
                                      {"Shots", "single"}
                                  };
            var quantity = 1;

            var cmd = new AddItem(
                cartId,
                itemId,
                menuItemId,
                preferences,
                quantity);

            WhenHelper.WhenExecuting(cmd);
        }

        [When(@"I add a large hot chocolate, skim milk, no whipped cream")]
        public void WhenIAddALargeHotChocolateSkimMilkNoWhippedCream()
        {
            var cartId = GetCartId();
            var itemId = Guid.NewGuid();
            var menuItemId = AggregateRootHelper.GetIdFor<MenuItem>("Hot Chocolate");
            var preferences = new Dictionary<string, string>()
                                  {
                                      {"Size", "large"},
                                      {"Milk", "skim"},
                                      {"Whipped Cream", "no"}
                                  };
            var quantity = 1;

            var cmd = new AddItem(
                cartId,
                itemId,
                menuItemId,
                preferences,
                quantity);

            WhenHelper.WhenExecuting(cmd);
        }

        [When(@"I place the order for take away")]
        public void WhenIPlaceTheOrderForTakeAway()
        {
            var cartId = AggregateRootHelper.GetIdFor<Cart>();

            var cmd = new PlaceOrder(cartId, Location.TakeAway);

            WhenHelper.WhenExecuting(cmd);
        }

        [When(@"I change the order location to take away")]
        public void WhenIChangeTheOrderLocationToTakeAway()
        {
            var cartId = AggregateRootHelper.GetIdFor<Cart>();

            var cmd = new ChangeLocation(cartId, Location.TakeAway);

            WhenHelper.WhenExecuting(cmd);
        }

        [When(@"I change the order location to in shop")]
        public void WhenIChangeTheOrderLocationToInShop()
        {
            var cartId = AggregateRootHelper.GetIdFor<Cart>();

            var cmd = new ChangeLocation(cartId, Location.InShop);

            WhenHelper.WhenExecuting(cmd);
        }

        [When(@"I abandon the cart")]
        public void WhenIAbandonTheCart()
        {
            var cartId = AggregateRootHelper.GetIdFor<Cart>();

            var cmd = new AbandonCart(cartId);

            WhenHelper.WhenExecuting(cmd);
        }

    }
}
