using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using Ncqrs.Spec;

namespace Restbucks
{
    [Specification]
    public class AllEventsFixture
    {

        [Then]
        public void all_events_are_marked_as_events()
        {
            var eventAssembly = typeof(IEvent).Assembly;
            var unmarkedEventTypes = eventAssembly.GetTypes()
                .Where(t => t.IsClass
                            && !t.IsAbstract
                            && !typeof(IEvent).IsAssignableFrom(t));

            if (!unmarkedEventTypes.Any()) return;

            var unmarkedEventTypeNames = unmarkedEventTypes
                .Select(t => t.ToString())
                .OrderBy(t => t);

            Assert.Fail("The following types are not marked with IEvent: \n{0}",
                        string.Join("\r\n", unmarkedEventTypeNames));
        }

        [Then]
        public void all_events_are_checked()
        {
            var eventAssembly = typeof(IEvent).Assembly;
            var testAssembly = Assembly.GetExecutingAssembly();

            var eventTypes = eventAssembly.GetTypes()
                .Where(t => t.IsClass
                            && !t.IsAbstract
                            && typeof(IEvent).IsAssignableFrom(t));

            var eventFixtureTypes = testAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract);

            var missingTests = new HashSet<Type>();

            foreach (var eventType in eventTypes)
            {
                var fixtureBaseType = typeof(EventSerializationFixture<>)
                    .MakeGenericType(eventType);

                var fixtureTypes = eventFixtureTypes
                    .Where(t => fixtureBaseType.IsAssignableFrom(t))
                    .ToArray();

                if (!fixtureTypes.Any())
                    missingTests.Add(eventType);
            }

            if (missingTests.Any())
                Assert.Fail("The following events are not being tested: \n{0}",
                            string.Join(Environment.NewLine, missingTests.Select(t => t.ToString()).OrderBy(t => t)));

        }

    }
}