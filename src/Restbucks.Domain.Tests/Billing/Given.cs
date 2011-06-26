using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace Restbucks.Billing
{
    [Binding]
    public class Given
    {

        //public static Guid GetOrderId()
        //{
        //    var orderCreated = GivenHelper.GetGivenEvents()
        //        .OfType<OrderCreated>()
        //        .Last();

        //    AggregateRootHelper.SetIdFor<Order>(orderCreated.OrderId);

        //    return orderCreated.OrderId;
        //}

        [Given(@"I have cancelled the order")]
        public void GivenIHaveCancelledTheOrder()
        {
            var orderId = AggregateRootHelper.GetIdFor<Order>();

            var e = new OrderCancelled(orderId);

            GivenHelper.GivenEvent<Order>(e);
        }

        [Given(@"I have paid for the order")]
        public void GivenIHavePaidForTheOrder()
        {
            var orderCreated = GivenHelper.GetGivenEvents()
                .OfType<OrderCreated>()
                .Single();

            var orderId = orderCreated.OrderId;
            var shoppingCartOrderId = orderCreated.ShoppingCardOrderId;
            
            AggregateRootHelper.SetIdFor<Order>(orderId);

            var e = new OrderPaid(orderId, shoppingCartOrderId);
            GivenHelper.GivenEvent<Order>(e);

        }

        [Given(@"I have created and cancelled an order")]
        public void GivenIHaveCreatedAndCancelledAnOrder()
        {
            new ShoppingCart.Given().GivenIHavePlacedAnOrder();
            GivenIHaveCancelledTheOrder();
        }

    
    }
}
