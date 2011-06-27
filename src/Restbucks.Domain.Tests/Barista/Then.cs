using System.Linq;
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
            var baristaOrderId = AggregateRootHelper.GetIdFor<Order>();
            var menuItemId = AggregateRootHelper.GetIdFor<Menu.MenuItem>("Cappuccino");
            
            var e = ThenHelper.GetEvent<OrderQueued>();

            e.OrderId.Should().Be.EqualTo(baristaOrderId);
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
            var baristaOrderId = AggregateRootHelper.GetIdFor<Order>();
            var cappucinoId = AggregateRootHelper.GetIdFor<Menu.MenuItem>("Cappuccino");

            var e = ThenHelper.GetEvent<OrderQueued>();

            e.OrderId.Should().Be.EqualTo(baristaOrderId);
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
            ScenarioContext.Current.Pending();
        }


    }
}
