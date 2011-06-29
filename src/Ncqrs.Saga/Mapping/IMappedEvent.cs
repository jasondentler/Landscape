using Ncqrs.Domain;

namespace Ncqrs.Saga.Mapping
{
    public interface IMappedEvent<TEvent>
    {

        IMappedSagaEvent<TEvent, TSaga> ToSaga<TSaga>()
            where TSaga : AggregateRoot, ISaga;

    }
}
