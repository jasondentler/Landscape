using System;
using System.Linq;
using System.Reflection;
using Cqrs;
using Cqrs.Eventing;

namespace Example.ReadModel.Tests
{
    public abstract class ConventionDenormalizerFixture<TEvent, TViewTable> : BaseDenormalizerFixture<TEvent>
        where TEvent : Event
    {
        protected ConventionDenormalizerFixture() :
            base(typeof(TViewTable).Name)
        {
        }

        protected override IHandle<TEvent> InstantiateHandler()
        {
            var viewTableType = typeof (TViewTable);
            var nestedTypes = viewTableType.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic);
            var handlerType = nestedTypes
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => typeof (IHandle<TEvent>).IsAssignableFrom(t))
                .Single();
            return (IHandle<TEvent>) Activator.CreateInstance(handlerType);
        }


    }
}
