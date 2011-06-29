using System;
using Ncqrs.Domain;

namespace Ncqrs.Saga.Mapping
{
    public interface IMappedEventToSagaWithConstructor<TEvent, TSaga>
        where TSaga : class, ISaga
    {

        ISagaEventExecutor<TEvent, TSaga> ToCallOn(Action<TEvent, TSaga> method);

    }
}
