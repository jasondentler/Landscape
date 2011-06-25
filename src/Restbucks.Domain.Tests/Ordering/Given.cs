using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Restbucks.Menu;
using TechTalk.SpecFlow;

namespace Restbucks.Ordering
{
    [Binding]
    public class Given
    {

        [Given(@"I have started an order")]
        public void GivenIHaveStartedAnOrder()
        {
            var orderId = Guid.NewGuid();
            DomainHelper.SetId<Order>(orderId);

            var e = new OrderCreated(orderId);
            DomainHelper.GivenEvent<Order>(e);
        }

        [Given(@"I have added a medium capuccino, skim milk, single shot")]
        public void GivenIHaveAddedAMediumCapuccinoSkimMilkSingleShot()
        {
            var orderId = DomainHelper.GetId<Order>();
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

            var e = new OrderItemAdded(
                orderId,
                orderItemId,
                productId,
                preferences,
                quantity);

            DomainHelper.GivenEvent<OrderItem>(e);
        }

        [Given(@"I have placed the order ""for here""")]
        public void GivenIHavePlacedTheOrderForHere()
        {
            var orderId = DomainHelper.GetId<Order>();

            var e = new OrderPlaced(orderId, Location.InShop);

            DomainHelper.GivenEvent<Order>(e);
        }

    }
}
