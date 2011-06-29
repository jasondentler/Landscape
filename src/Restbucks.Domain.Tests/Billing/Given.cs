using System;
using System.Linq;
using Restbucks.Sagas;
using TechTalk.SpecFlow;

namespace Restbucks.Billing
{
    [Binding]
    public class Given
    {

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
            var deliverySagaId = AggregateRootHelper.GetOrCreateId<DeliverySaga>();

            var orderCreated = GivenHelper.GetGivenEvents()
                .OfType<OrderPlaced>()
                .Single();

            var orderId = orderCreated.OrderId;
            var shoppingCartOrderId = orderCreated.ShoppingCartOrderId;
            
            var e = new OrderPaid(orderId, shoppingCartOrderId, deliverySagaId);
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
