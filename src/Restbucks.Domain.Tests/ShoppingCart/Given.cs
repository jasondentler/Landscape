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
            AggregateRootHelper.SetIdFor<Order>(orderId);

            var e = new OrderCreated(orderId);
            GivenHelper.GivenEvent<Order>(e);
        }

        [Given(@"I have added a medium cappuccino, skim milk, single shot")]
        public void GivenIHaveAddedAMediumCappuccinoSkimMilkSingleShot()
        {
            var orderId = AggregateRootHelper.GetIdFor<Order>();
            var orderItemId = Guid.NewGuid();
            var menuItemId = AggregateRootHelper.GetIdFor<MenuItem>("Cappuccino");
            var preferences = new Dictionary<string, string>()
                                  {
                                      {"Size", "medium"},
                                      {"Milk", "skim"},
                                      {"Shots", "single"}
                                  };
            var quantity = 1;

            AggregateRootHelper.SetIdFor<OrderItem>(orderItemId);

            var e = new OrderItemAdded(
                orderId,
                orderItemId,
                menuItemId,
                preferences,
                quantity);

            GivenHelper.GivenEvent<Order>(e);
        }

        [Given(@"I have placed the order ""for here""")]
        public void GivenIHavePlacedTheOrderForHere()
        {
            var orderId = AggregateRootHelper.GetIdFor<Order>();

            var itemInfos = ThenHelper.GetAggregateRootEvents(orderId)
                .OfType<OrderItemAdded>()
                .Select(i => new OrderItemInfo(
                                 i.OrderItemId, i.MenuItemId, i.Preferences, i.Quantity))
                .ToArray();

            var e = new OrderPlaced(orderId, Location.InShop, itemInfos);

            GivenHelper.GivenEvent<Order>(e);
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
