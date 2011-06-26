using System;
using TechTalk.SpecFlow;

namespace Restbucks
{
    public class AggregateRootHelper : ScenarioContextHelper
    {
        private class WrappedValue<T>
        {
            public WrappedValue(T guid)
            {
                Value = guid;
            }

            public T Value { get; set; }
        }

        public static Guid GetEventSourceId<T>()
        {
            return Get<WrappedValue<Guid>>(typeof(T).ToString()).Value;
        }

        public static void SetIdFor<T>(Guid id, params string[] naturalId)
        {
            var key = typeof(T) + String.Join(" ", naturalId);
            Set(new WrappedValue<Guid>(id), key);
            Set(new WrappedValue<Guid>(id), typeof(T).ToString());
        }

        public static Guid GetIdFor<T>(params string[] naturalId)
        {
            var key = typeof(T) + String.Join(" ", naturalId);
            return Get<WrappedValue<Guid>>(key).Value;
        }

        public static bool IdExists<T>(params string[] naturalId)
        {
            var key = typeof(T) + String.Join(" ", naturalId);
            return ScenarioContext.Current.ContainsKey(key);
        }
    }
}
