using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Restbucks.Barista
{
    [Binding]
    public class When
    {

        [When(@"I queue an order for the barista")]
        public void WhenIQueueAnOrderForTheBarista()
        {
            var cappuccinoId = AggregateRootHelper.GetIdFor<Menu.MenuItem>("Cappuccino");

            var cmd = new QueueOrder(
                Guid.NewGuid(),
                Location.TakeAway,
                new[]
                    {
                        new OrderItemInfo(cappuccinoId,
                                          new Dictionary<string, string>()
                                              {
                                                  {"Size", "medium"},
                                                  {"Milk", "skim"},
                                                  {"Shots", "single"}
                                              },
                                          1)
                    });

            WhenHelper.WhenExecuting(cmd);

        }

        [When(@"I begin preparing the order")]
        public void WhenIBeginPreparingTheOrder()
        {
            var orderId = AggregateRootHelper.GetIdFor<Order>();
            var cmd = new BeginPreparingOrder(orderId);
            WhenHelper.WhenExecuting(cmd);
        }

        [When(@"I finish preparing the order")]
        public void WhenIFinishPreparingTheOrder()
        {
            var orderId = AggregateRootHelper.GetIdFor<Order>();
            var cmd = new FinishPreparingOrder(orderId);
            WhenHelper.WhenExecuting(cmd);
        }


    }
}
