using System;
using Ncqrs.Saga.Mapping.Impl;

namespace Ncqrs.Saga.Mapping
{
    public static class Map
    {

        public static IMappedEvent<TEvent> Event<TEvent>()
        {
            return new MappedEvent<TEvent>();
        }


    }
}
