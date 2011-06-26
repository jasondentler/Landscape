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
            var orderCreated = DomainHelper.GetGivenEvents()
                .OfType<OrderCreated>()
                .Single();

            var orderId = orderCreated.OrderId;

            DomainHelper.SetId<Order>(orderId);

            var cmd = new PayWithCreditCard(orderId, "John Doe", "5444444444444444", orderCreated.OrderTotal);

            DomainHelper.WhenExecuting(cmd);
        }

    }
}
