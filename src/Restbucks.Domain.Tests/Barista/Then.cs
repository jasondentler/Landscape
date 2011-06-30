using System.Linq;
using Restbucks.Sagas;
using SharpTestsEx;
using TechTalk.SpecFlow;

namespace Restbucks.Barista
{
    [Binding]
    public class Then
    {

        [Then(@"the barista receives the order")]
        public void ThenTheBaristaReceivesTheOrder()
        {
            var orderId = AggregateRootHelper.GetIdFor<Order>();
            var menuItemId = AggregateRootHelper.GetIdFor<Menu.MenuItem>("Cappuccino");
            var deliverySagaId = AggregateRootHelper.GetIdFor<DeliverySaga>();

            var e = ThenHelper.GetEvent<OrderQueued>();

            e.OrderId.Should().Be.EqualTo(orderId);
            e.DeliverySagaId.Should().Be.EqualTo(deliverySagaId);
            e.Items.Length.Should().Be.EqualTo(1);

            var item = e.Items.Single();

            item.MenuItemId.Should().Be.EqualTo(menuItemId);
            item.Preferences.Count.Should().Be.EqualTo(3);
            item.Preferences["Size"].Should().Be.EqualTo("medium");
            item.Preferences["Milk"].Should().Be.EqualTo("skim");
            item.Preferences["Shots"].Should().Be.EqualTo("single");
            item.Quantity.Should().Be.EqualTo(1);

            

        }

        [Then(@"the order is queued for the barista")]
        public void ThenTheOrderIsQueuedForTheBarista()
        {
            var orderId = AggregateRootHelper.GetIdFor<Order>();
            var cappucinoId = AggregateRootHelper.GetIdFor<Menu.MenuItem>("Cappuccino");
            var deliverySagaId = AggregateRootHelper.GetIdFor<DeliverySaga>();
            var e = ThenHelper.GetEvent<OrderQueued>();
            
            e.OrderId.Should().Be.EqualTo(orderId);
            e.DeliverySagaId.Should().Be.EqualTo(deliverySagaId);
            e.Items.Length.Should().Be.EqualTo(1);

            var item = e.Items.Single();

            item.MenuItemId.Should().Be.EqualTo(cappucinoId);
            item.Preferences.Count.Should().Be.EqualTo(3);
            item.Preferences["Size"].Should().Be.EqualTo("medium");
            item.Preferences["Milk"].Should().Be.EqualTo("skim");
            item.Preferences["Shots"].Should().Be.EqualTo("single");
            item.Quantity.Should().Be.EqualTo(1);
        }

        [Then(@"the order is being prepared")]
        public void ThenTheOrderIsBeingPrepared()
        {
            var orderId = AggregateRootHelper.GetIdFor<Order>();

            var e = ThenHelper.GetEvent<OrderBeingPrepared>();

            e.OrderId.Should().Be.EqualTo(orderId);
        }

        [Then(@"the order is prepared")]
        public void ThenTheOrderIsPrepared()
        {
            var orderId = AggregateRootHelper.GetIdFor<Order>();
            var deliverySagaId = AggregateRootHelper.GetIdFor<DeliverySaga>();
            var e = ThenHelper.GetEvent<OrderPrepared>();
            e.DeliverySagaId.Should().Be.EqualTo(deliverySagaId);
            e.OrderId.Should().Be.EqualTo(orderId);
        }


        [Then(@"the order is delivered")]
        public void ThenTheOrderIsDelivered()
        {
            var orderId = AggregateRootHelper.GetIdFor<Order>();
            var e = ThenHelper.GetEvent<OrderDelivered>();
            e.OrderId.Should().Be.EqualTo(orderId);
        }

    }
}
