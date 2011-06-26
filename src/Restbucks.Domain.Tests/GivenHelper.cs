using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs;
using Ncqrs.Eventing;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Storage;
using Ncqrs.Spec;

namespace Restbucks
{
    public class GivenHelper : ScenarioContextHelper
    {
        private const string GivenEventsKey = "givenEvents";

        public static void GivenEvent<T>(object @event)
        {
            GivenEvent(AggregateRootHelper.GetEventSourceId<T>(), @event);
        }

        public static void GivenEvent(Guid eventSourceId, object @event)
        {
            IEnumerable<UncommittedEvent> events;
            using (var context = new EventContext())
            {
                var store = NcqrsEnvironment.Get<IEventStore>();
                var existingEvents = store.ReadFrom(eventSourceId, 0, Int64.MaxValue);
                long maxEventSequence = 0;
                if (existingEvents.Any())
                    maxEventSequence = existingEvents.Max(e => e.EventSequence);

                var stream = Prepare.Events(@event)
                    .ForSourceUncomitted(eventSourceId, Guid.NewGuid(), (int)maxEventSequence + 1);

                store.Store(stream);

                var bus = NcqrsEnvironment.Get<IEventBus>();
                bus.Publish(stream);

                events = context.Events;
            }

            CaptureGivenEvents(new [] {@event});
            CaptureGivenEvents(events.Select(e => e.Payload));

        }

        private static void CaptureGivenEvents(IEnumerable<object> events)
        {
            List<object> givenEvents;
            if (!ContainsKey(GivenEventsKey))
            {
                givenEvents = new List<object>();
                Set(givenEvents, GivenEventsKey);
            }
            else
            {
                givenEvents = Get<List<object>>(GivenEventsKey);
            }

            foreach (var e in events)
            {
                Console.Write("\tGiven ");
                WriteOutObject(e);
                givenEvents.Add(e);
            }
        }

        public static IEnumerable<object> GetGivenEvents()
        {
            return Get<List<object>>(GivenEventsKey).ToArray();
        }
    }
}
