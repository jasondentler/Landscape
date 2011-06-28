using System;

namespace Ncqrs.Saga.Mapping
{
    public interface IMappedSagaEvent<TEvent, TSaga>
        where TSaga : class, ISaga
    {

        ISagaEventExecutor<TEvent, TSaga> CreateNew(Func<TEvent, TSaga> createNewSaga);
        IMappedEventToSaga<TEvent, TSaga> WithId(Func<TEvent, Guid> getSagaId);

    }
}
