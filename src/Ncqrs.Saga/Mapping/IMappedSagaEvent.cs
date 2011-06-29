using System;

namespace Ncqrs.Saga.Mapping
{
    public interface IMappedSagaEvent<TEvent, TSaga>
        where TSaga : class, ISaga
    {

        IMappedEventToSaga<TEvent, TSaga> WithId(Func<TEvent, Guid> getSagaId);

    }
}
