using System;
using Ncqrs.Domain;

namespace Ncqrs.Saga.Mapping
{
    public interface IMappedSagaEvent<TEvent, TSaga>
        where TSaga : AggregateRoot, ISaga
    {

        IMappedEventToSaga<TEvent, TSaga> WithId(Func<TEvent, Guid> getSagaId);

    }
}
