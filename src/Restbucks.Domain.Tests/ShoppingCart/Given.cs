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
            DomainHelper.SetId<Order>(orderId);

            var e = new OrderCreated(orderId);
            DomainHelper.GivenEvent<Order>(e);
        }

        [Given(@"I have added a medium cappuccino, skim milk, single shot")]
        public void GivenIHaveAddedAMediumCappuccinoSkimMilkSingleShot()
        {
            var orderId = DomainHelper.GetId<Order>();
            var orderItemId = Guid.NewGuid();
            var menuItemId = DomainHelper.GetId<MenuItem>("Cappuccino");
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
                menuItemId,
                preferences,
                quantity);

            DomainHelper.GivenEvent<Order>(e);
        }

        [Given(@"I have placed the order ""for here""")]
        public void GivenIHavePlacedTheOrderForHere()
        {
            var orderId = DomainHelper.GetId<Order>();

            var e = new OrderPlaced(orderId, Location.InShop);

            DomainHelper.GivenEvent<Order>(e);
        }

        [Given(@"I have cancelled the order")]
        public void GivenIHaveCancelledTheOrder()
        {
            var orderId = DomainHelper.GetId<Order>();

            var e = new OrderCancelled(orderId);

            DomainHelper.GivenEvent<Order>(e);
        }

        [Given(@"I have placed an order")]
        public void GivenIHavePlacedAnOrder()
        {
            GivenIHaveCreatedAnOrder();
            GivenIHaveAddedAMediumCappuccinoSkimMilkSingleShot();
            GivenIHavePlacedTheOrderForHere();
        }

        [Given(@"I have created and cancelled an order")]
        public void GivenIHaveCreatedAndCancelledAnOrder()
        {
            GivenIHaveCreatedAnOrder();
            GivenIHaveAddedAMediumCappuccinoSkimMilkSingleShot();
            GivenIHavePlacedAnOrder();
            GivenIHaveCancelledTheOrder();
        }

    }
}
