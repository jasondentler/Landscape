using System;
using System.Collections.Generic;
using Restbucks.Menu;
using TechTalk.SpecFlow;

namespace Restbucks.Ordering
{
    [Binding]
    public class When
    {

        private Guid GetOrderId()
        {
            if (DomainHelper.IdExists<Order>())
                return DomainHelper.GetId<Order>();
            var orderId = Guid.NewGuid();
            DomainHelper.SetId<Order>(orderId);
            return orderId;
        }

        [When(@"I add a medium capuccino, skim milk, single shot")]
        public void WhenIAddAMediumCapuccinoSkimMilkSingleShot()
        {

            var orderId = GetOrderId();
            var orderItemId = Guid.NewGuid();
            var productId = DomainHelper.GetId<Product>("Capuccino");
            var preferences = new Dictionary<string, string>()
                                  {
                                      {"Size", "medium"},
                                      {"Milk", "skim"},
                                      {"Shots", "single"}
                                  };
            var quantity = 1;

            DomainHelper.SetId<OrderItem>(orderItemId);

            var cmd = new AddOrderItem(
                orderId,
                orderItemId,
                productId,
                preferences,
                quantity);

            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I add a large hot chocolate, skim milk, no whipped cream")]
        public void WhenIAddALargeHotChocolateSkimMilkNoWhippedCream()
        {
            var orderId = GetOrderId();
            var orderItemId = Guid.NewGuid();
            var productId = DomainHelper.GetId<Product>("Hot Chocolate");
            var preferences = new Dictionary<string, string>()
                                  {
                                      {"Size", "large"},
                                      {"Milk", "skim"},
                                      {"Whipped Cream", "no"}
                                  };
            var quantity = 1;

            DomainHelper.SetId<OrderItem>(orderItemId);

            var cmd = new AddOrderItem(
                orderId,
                orderItemId,
                productId,
                preferences,
                quantity);

            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I place the order for take away")]
        public void WhenIPlaceTheOrderForTakeAway()
        {
            var orderId = DomainHelper.GetId<Order>();

            var cmd = new PlaceOrder(orderId, Location.TakeAway);

            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I change the order location to take away")]
        public void WhenIChangeTheOrderLocationToTakeAway()
        {
            var orderId = DomainHelper.GetId<Order>();

            var cmd = new ChangeOrderLocation(orderId, Location.TakeAway);

            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I change the order location to in shop")]
        public void WhenIChangeTheOrderLocationToInShop()
        {
            var orderId = DomainHelper.GetId<Order>();

            var cmd = new ChangeOrderLocation(orderId, Location.InShop);

            DomainHelper.WhenExecuting(cmd);
        }

    }
}
