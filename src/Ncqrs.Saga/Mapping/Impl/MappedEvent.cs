using Ncqrs.Domain;

namespace Ncqrs.Saga.Mapping.Impl
{
    public class MappedEvent<TEvent>
        : IMappedEvent<TEvent>
    {
        public IMappedSagaEvent<TEvent, TSaga> ToSaga<TSaga>() where TSaga : AggregateRoot, ISaga
        {
            return new MappedSagaEvent<TEvent, TSaga>();
        }
    }
}
