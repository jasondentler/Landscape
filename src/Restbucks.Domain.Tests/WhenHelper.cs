using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing;
using Ncqrs.Spec;
using TechTalk.SpecFlow;

namespace Restbucks
{
    public class WhenHelper : ScenarioContextHelper
    {
        public static IEnumerable<UncommittedEvent> When(Action action)
        {
            try
            {
                IEnumerable<UncommittedEvent> events;
                using (var context = new EventContext())
                {
                    action();
                    events = context.Events;
                }
                ScenarioContext.Current.Set(new HashSet<object>(), ThenHelper.TestedEventsKey);
                ScenarioContext.Current.Set(events);
                return events;
            }
            catch (Exception e)
            {
                ScenarioContext.Current.Set(e, ExceptionKey);
                return new UncommittedEvent[0];
            }
        }

        public static IEnumerable<UncommittedEvent> WhenExecuting(ICommand command)
        {
            Console.Write("\tWhen ");
            WriteOutObject(command);

            ScenarioContext.Current.Set(command);
            var stream = When(() =>
                                  {
                                      var cmdService = NcqrsEnvironment.Get<ICommandService>();
                                      cmdService.Execute(command);
                                  });
            foreach (var e in stream.Select(e => e.Payload))
            {
                Console.Write("\tResulting ");
                WriteOutObject(e);
            }
            if (ThenHelper.HasException())
            {
                Console.Write("\tResulting ");
                Console.WriteLine(ThenHelper.GetException().ToString());
            }
            return stream;
        }
    }
}
