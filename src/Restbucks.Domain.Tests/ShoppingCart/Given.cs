using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Restbucks.Menu;
using TechTalk.SpecFlow;

namespace Restbucks.ShoppingCart
{
    [Binding]
    public class Given
    {

        [Given(@"I have created an order")]
        [Given(@"I have started an order")]
        public void GivenIHaveCreatedAnOrder()
        {
            var orderId = Guid.NewGuid();
            AggregateRootHelper.SetIdFor<Cart>(orderId);

            var e = new CartCreated(orderId);
            GivenHelper.GivenEvent<Cart>(e);
        }

        [Given(@"I have added a medium cappuccino, skim milk, single shot")]
        public void GivenIHaveAddedAMediumCappuccinoSkimMilkSingleShot()
        {
            var orderId = AggregateRootHelper.GetIdFor<Cart>();
            var orderItemId = Guid.NewGuid();
            var menuItemId = AggregateRootHelper.GetIdFor<MenuItem>("Cappuccino");
            var preferences = new Dictionary<string, string>()
                                  {
                                      {"Size", "medium"},
                                      {"Milk", "skim"},
                                      {"Shots", "single"}
                                  };
            var quantity = 1;

            AggregateRootHelper.SetIdFor<CartItem>(orderItemId);

            var e = new ItemAdded(
                orderId,
                orderItemId,
                menuItemId,
                preferences,
                quantity);

            GivenHelper.GivenEvent<Cart>(e);
        }

        [Given(@"I have placed the order ""for here""")]
        public void GivenIHavePlacedTheOrderForHere()
        {
            var orderId = AggregateRootHelper.GetIdFor<Cart>();

            var itemInfos = ThenHelper.GetAggregateRootEvents(orderId)
                .OfType<ItemAdded>()
                .Select(i => new OrderItemInfo(
                                 i.ItemId, i.MenuItemId, i.Preferences, i.Quantity))
                .ToArray();

            var e = new OrderPlaced(orderId, Location.InShop, itemInfos);

            GivenHelper.GivenEvent<Cart>(e);
        }


        [Given(@"I have placed an order")]
        public void GivenIHavePlacedAnOrder()
        {
            GivenIHaveCreatedAnOrder();
            GivenIHaveAddedAMediumCappuccinoSkimMilkSingleShot();
            GivenIHavePlacedTheOrderForHere();
        }


    }
}
