using System;

namespace Ncqrs.Saga.Mapping
{
    public interface IMappedEventToSaga<TEvent, TSaga>
        where TSaga : class, ISaga
    {

        ISagaEventExecutor<TEvent, TSaga> ToCallOn(Action<TEvent, TSaga> method);

    }
}
