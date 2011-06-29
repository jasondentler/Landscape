using Ncqrs.Domain;

namespace Ncqrs.Saga.Mapping
{
    public interface ISagaEventExecutor<TEvent, TSaga>
        where TSaga : class, ISaga
    {

        void Transition(TEvent @event);
        
    }
}
