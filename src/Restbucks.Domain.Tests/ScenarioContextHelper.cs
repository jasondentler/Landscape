using System;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace Restbucks
{
    public class ScenarioContextHelper  
    {
        protected static bool ContainsKey(string key)
        {
            return ScenarioContext.Current.ContainsKey(key);
        }

        protected static T Get<T>() where T : class
        {
            return ScenarioContext.Current.Get<T>();
        }

        protected static T Get<T>(string key) where T : class
        {
            return ScenarioContext.Current.Get<T>(key);
        }

        protected static void Set<T>(T item) where T : class
        {
            ScenarioContext.Current.Set(item);
        }

        protected static void Set<T>(T item, string key) where T : class
        {
            ScenarioContext.Current.Set(item, key);
        }

        public static void WriteOutObject(object @event)
        {
            var jsonEvent = JsonConvert.SerializeObject(@event);
            Console.WriteLine("{0}: {1}",
                              @event.GetType(),
                              jsonEvent);
        }

        protected const string ExceptionKey = "caughtException";
        protected const string TestedEventsKey = "testedEvents";
    }
}
