using System.Collections.Generic;
using Cqrs;
using Cqrs.Eventing;

namespace Example.ReadModel.Tests
{
    public abstract class BaseQueryFixture<TResult> : SimpleDataFixture
    {

        private object _handler;
        

        protected readonly List<Event> GivenEvents = new List<Event>();
        protected TResult Result;

        protected BaseQueryFixture(string tableName, params string[] tableNames)
            : base(tableName, tableNames)
        {
        }

        protected override void OnSetup()
        {
            base.OnSetup();
            _handler = InstantiateHandler();
        }

        protected void GivenEvent<T>(T e)
            where T : Event
        {
            GivenEvents.Add(e);
            var handler = (IHandle<T>) _handler;
            handler.Handle(e);
        }

        protected abstract object InstantiateHandler();

        protected abstract TResult WhenQuerying();

        protected override void When()
        {
            Result = WhenQuerying();
        }


    }
}
