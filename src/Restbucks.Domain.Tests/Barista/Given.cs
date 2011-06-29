using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Restbucks.Sagas;
using TechTalk.SpecFlow;

namespace Restbucks.Barista
{
    [Binding]
    public class Given
    {

        [Given(@"an order has been queued for the barista")]
        public void GivenAnOrderHasBeenQueuedForTheBarista()
        {
            new ShoppingCart.Given().GivenIHavePlacedAnOrder();
        }

        [Given(@"I have started preparing the order")]
        public void GivenIHaveStartedPreparingTheOrder()
        {
            var orderId = AggregateRootHelper.GetIdFor<Order>();

            var e = new OrderBeingPrepared(orderId);
            GivenHelper.GivenEvent<Order>(e);
        }

        [Given(@"I have prepared the order")]
        public void GivenIHavePreparedTheOrder()
        {
            var orderQueued = GivenHelper.GetGivenEvents()
                .OfType<OrderQueued>()
                .LastOrDefault();

            var deliverySagaId = AggregateRootHelper.GetOrCreateId<DeliverySaga>();

            var orderId = AggregateRootHelper.GetIdFor<Order>();

            var e = new OrderPrepared(orderId, deliverySagaId);
            GivenHelper.GivenEvent<Order>(e);
        }


    }
}
