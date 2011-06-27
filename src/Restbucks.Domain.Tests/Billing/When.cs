using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace Restbucks.Billing
{
    [Binding]
    public class When
    {

        [When(@"I cancel the order")]
        public void WhenICancelTheOrder()
        {
            var orderId = AggregateRootHelper.GetIdFor<Order>();

            var cmd = new CancelOrder(orderId);

            WhenHelper.WhenExecuting(cmd);
        }



        [When(@"I pay with a credit card")]
        public void WhenIPayWithACreditCard()
        {
            var orderCreated = GivenHelper.GetGivenEvents()
                .OfType<OrderPlaced>()
                .Single();

            var orderId = orderCreated.OrderId;

            var cmd = new PayWithCreditCard(orderId, "John Doe", "5444444444444444", orderCreated.OrderTotal);

            WhenHelper.WhenExecuting(cmd);
        }

        [When(@"I pay the wrong amount")]
        public void WhenIPayTheWrongAmount()
        {
            var orderCreated = GivenHelper.GetGivenEvents()
                .OfType<OrderPlaced>()
                .Single();

            var orderId = orderCreated.OrderId;

            var cmd = new PayWithCreditCard(orderId, "John Doe", "5444444444444444", orderCreated.OrderTotal - 0.50M);

            WhenHelper.WhenExecuting(cmd);
        }

    }
}
