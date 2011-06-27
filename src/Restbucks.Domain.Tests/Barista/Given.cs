using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            GivenIQueueTheOrderForTheBarista();
        }

        public void GivenIQueueTheOrderForTheBarista()
        {
            var orderId = Guid.NewGuid();

            var e = new OrderQueued(
                orderId,
                GetOrderLocation(),
                GetOrderItems().ToArray());

            GivenHelper.GivenEvent(orderId, e);
        }

        private Location GetOrderLocation()
        {
            var cartId = AggregateRootHelper.GetIdFor<ShoppingCart.Cart>();
            var events = ThenHelper.GetAggregateRootEvents(cartId).ToArray();

            var orderPlaced = events.OfType<ShoppingCart.OrderPlaced>().Single();
            var locationChanged = events.OfType<ShoppingCart.LocationChanged>().LastOrDefault();

            if (locationChanged != null)
                return locationChanged.Location;
            return orderPlaced.Location;
        }

        private IEnumerable<OrderItemInfo> GetOrderItems()
        {
            var cartId = AggregateRootHelper.GetIdFor<ShoppingCart.Cart>();
            var addItemEvents = ThenHelper.GetAggregateRootEvents(cartId)
                .OfType<ShoppingCart.ItemAdded>();

            foreach (var item in addItemEvents)
                yield return new OrderItemInfo(
                    item.MenuItemId,
                    item.Preferences.ToDictionary(i => i.Key, i => i.Value),
                    item.Quantity);
        }


    }
}
