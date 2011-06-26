using System.Linq;
using TechTalk.SpecFlow;

namespace Restbucks.Billing
{
    [Binding]
    public class When
    {

        [When(@"I pay with a credit card")]
        public void WhenIPayWithACreditCard()
        {
            var orderCreated = GivenHelper.GetGivenEvents()
                .OfType<OrderCreated>()
                .Single();

            var orderId = orderCreated.OrderId;

            AggregateRootHelper.SetIdFor<Order>(orderId);

            var cmd = new PayWithCreditCard(orderId, "John Doe", "5444444444444444", orderCreated.OrderTotal);

            WhenHelper.WhenExecuting(cmd);
        }

    }
}
