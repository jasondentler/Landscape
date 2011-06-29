using System;
using Ncqrs.Domain;

namespace Ncqrs.Saga.Mapping
{
    public interface IMappedEventToSagaWithConstructor<TEvent, TSaga>
        where TSaga : AggregateRoot, ISaga
    {

        ISagaEventExecutor<TEvent, TSaga> ToCallOn(Action<TEvent, TSaga> method);

    }
}
