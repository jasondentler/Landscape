using System;
using System.Collections.Generic;
using System.Linq;
using Restbucks.Menu;
using TechTalk.SpecFlow;

namespace Restbucks.ShoppingCart
{
    [Binding]
    public class Given
    {

        [Given(@"I have created a cart")]
        [Given(@"I have started a cart")]
        public void GivenIHaveCreatedAnCart()
        {
            var cartId = Guid.NewGuid();

            var e = new CartCreated(cartId);
            GivenHelper.GivenEvent(cartId, e);
        }

        [Given(@"I have added a medium cappuccino, skim milk, single shot")]
        public void GivenIHaveAddedAMediumCappuccinoSkimMilkSingleShot()
        {
            var cartId = AggregateRootHelper.GetIdFor<Cart>();
            var itemId = Guid.NewGuid();
            var menuItemId = AggregateRootHelper.GetIdFor<MenuItem>("Cappuccino");
            var preferences = new Dictionary<string, string>()
                                  {
                                      {"Size", "medium"},
                                      {"Milk", "skim"},
                                      {"Shots", "single"}
                                  };
            var quantity = 1;


            var e = new ItemAdded(
                cartId,
                itemId,
                menuItemId,
                preferences,
                quantity);

            GivenHelper.GivenEvent<Cart>(e);
        }

        [Given(@"I have placed the order ""for here""")]
        public void GivenIHavePlacedTheOrderForHere()
        {
            var cartId = AggregateRootHelper.GetIdFor<Cart>();

            var itemInfos = ThenHelper.GetAggregateRootEvents(cartId)
                .OfType<ItemAdded>()
                .Select(i => new OrderItemInfo(
                                 i.ItemId, i.MenuItemId, i.Preferences, i.Quantity))
                .ToArray();

            var e = new OrderPlaced(cartId, Location.InShop, itemInfos);

            GivenHelper.GivenEvent<Cart>(e);
        }


        [Given(@"I have placed an order")]
        public void GivenIHavePlacedAnOrder()
        {
            GivenIHaveCreatedAnCart();
            GivenIHaveAddedAMediumCappuccinoSkimMilkSingleShot();
            GivenIHavePlacedTheOrderForHere();
        }


    }
}
