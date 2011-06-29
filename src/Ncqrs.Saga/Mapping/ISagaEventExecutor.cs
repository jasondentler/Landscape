using Ncqrs.Domain;

namespace Ncqrs.Saga.Mapping
{
    public interface ISagaEventExecutor<TEvent, TSaga>
        where TSaga : AggregateRoot, ISaga
    {

        void Transition(TEvent @event);
        
    }
}
