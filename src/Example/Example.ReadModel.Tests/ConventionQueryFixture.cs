using System;
using System.Linq;
using System.Reflection;

namespace Example.ReadModel.Tests
{
    public abstract class ConventionQueryFixture<TResult, TViewTable> : BaseQueryFixture<TResult>
    {
        protected ConventionQueryFixture() :
            base(typeof(TViewTable).Name)
        {
        }

        protected override object InstantiateHandler()
        {
            var viewTableType = typeof (TViewTable);
            var nestedTypes = viewTableType.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic);
            var handlerType = nestedTypes
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => t.GetInterfaces().Any(i => i.Name.StartsWith("IHandle")))
                .Single();
            return Activator.CreateInstance(handlerType);
        }


    }
}
