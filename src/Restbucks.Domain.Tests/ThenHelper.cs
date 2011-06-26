using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Storage;
using TechTalk.SpecFlow;

namespace Restbucks
{
    public class ThenHelper : ScenarioContextHelper
    {
        public static bool HasException()
        {
            return ScenarioContext.Current.ContainsKey(ExceptionKey);
        }

        public static Exception GetException()
        {
            return HasException() ? ScenarioContext.Current.Get<Exception>(ExceptionKey) : null;
        }

        public static T GetException<T>()
            where T : Exception
        {
            return (T)GetException();
        }

        public static IEnumerable<object> GetAggregateRootEvents(Guid eventSourceId)
        {
            var store = NcqrsEnvironment.Get<IEventStore>();
            var existingEvents = store.ReadFrom(eventSourceId, 0, long.MaxValue);
            return existingEvents.Select(e => e.Payload);
        }

        public static IEnumerable<object> GetResultingEvents()
        {
            return ScenarioContext.Current.Get<IEnumerable<UncommittedEvent>>()
                .Select(e => e.Payload);
        }
        
        public static T GetEvent<T>()
        {
            var @event = GetResultingEvents()
                .OfType<T>()
                .SingleOrDefault();
            AddToTestedEvents(new[] {@event});
            return @event;
        }

        private static void AddToTestedEvents<T>(IEnumerable<T> @events)
        {
            var testedEvents = ScenarioContext.Current.Get<HashSet<object>>(TestedEventsKey);
            foreach (var @event in events)
                testedEvents.Add(@event);
        }

        public static IEnumerable<object> GetUntestedEvents()
        {
            var testedEvents = ScenarioContext.Current.Get<HashSet<object>>(TestedEventsKey);
            var untestedEvents = GetResultingEvents().Except(testedEvents);
            return untestedEvents.ToArray();
        }

    }
}