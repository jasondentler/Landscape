using System;
using System.Collections.Generic;
using Restbucks.Menu;
using TechTalk.SpecFlow;

namespace Restbucks.ShoppingCart
{
    [Binding]
    public class When
    {

        private Guid GetOrderId()
        {
            if (AggregateRootHelper.IdExists<Order>())
                return AggregateRootHelper.GetIdFor<Order>();
            var orderId = Guid.NewGuid();
            AggregateRootHelper.SetIdFor<Order>(orderId);
            return orderId;
        }

        [When(@"I add a medium cappuccino, skim milk, single shot")]
        public void WhenIAddAMediumCappuccinoSkimMilkSingleShot()
        {

            var orderId = GetOrderId();
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

            var cmd = new AddOrderItem(
                orderId,
                orderItemId,
                menuItemId,
                preferences,
                quantity);

            WhenHelper.WhenExecuting(cmd);
        }

        [When(@"I add a large hot chocolate, skim milk, no whipped cream")]
        public void WhenIAddALargeHotChocolateSkimMilkNoWhippedCream()
        {
            var orderId = GetOrderId();
            var orderItemId = Guid.NewGuid();
            var menuItemId = AggregateRootHelper.GetIdFor<MenuItem>("Hot Chocolate");
            var preferences = new Dictionary<string, string>()
                                  {
                                      {"Size", "large"},
                                      {"Milk", "skim"},
                                      {"Whipped Cream", "no"}
                                  };
            var quantity = 1;

            AggregateRootHelper.SetIdFor<OrderItem>(orderItemId);

            var cmd = new AddOrderItem(
                orderId,
                orderItemId,
                menuItemId,
                preferences,
                quantity);

            WhenHelper.WhenExecuting(cmd);
        }

        [When(@"I place the order for take away")]
        public void WhenIPlaceTheOrderForTakeAway()
        {
            var orderId = AggregateRootHelper.GetIdFor<Order>();

            var cmd = new PlaceOrder(orderId, Location.TakeAway);

            WhenHelper.WhenExecuting(cmd);
        }

        [When(@"I change the order location to take away")]
        public void WhenIChangeTheOrderLocationToTakeAway()
        {
            var orderId = AggregateRootHelper.GetIdFor<Order>();

            var cmd = new ChangeOrderLocation(orderId, Location.TakeAway);

            WhenHelper.WhenExecuting(cmd);
        }

        [When(@"I change the order location to in shop")]
        public void WhenIChangeTheOrderLocationToInShop()
        {
            var orderId = AggregateRootHelper.GetIdFor<Order>();

            var cmd = new ChangeOrderLocation(orderId, Location.InShop);

            WhenHelper.WhenExecuting(cmd);
        }

    }
}
