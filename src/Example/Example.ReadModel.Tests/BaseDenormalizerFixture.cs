using System;
using System.Collections.Generic;
using Cqrs;
using Cqrs.Eventing;

namespace Example.ReadModel.Tests
{
    public abstract class BaseDenormalizerFixture<TEvent> : SimpleDataFixture
        where TEvent : Event
    {

        private IHandle<TEvent> _handler;

        protected readonly List<Event> GivenEvents = new List<Event>();
        protected TEvent Event;

        protected BaseDenormalizerFixture(string tableName, params string[] tableNames)
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

        protected abstract TEvent WhenEvent();

        protected abstract IHandle<TEvent> InstantiateHandler();

        protected override void When()
        {
            Event = WhenEvent();
            _handler.Handle(Event);
        }

    }
}
