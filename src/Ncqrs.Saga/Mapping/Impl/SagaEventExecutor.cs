using System;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Storage;

namespace Ncqrs.Saga.Mapping.Impl
{
    public class SagaEventExecutor<TEvent, TSaga>
        : ISagaEventExecutor<TEvent, TSaga>
        where TSaga : AggregateRoot, ISaga
    {

        private readonly Func<TEvent, Guid> _getSagaId;
        private readonly Action<TEvent, TSaga> _method;

        public SagaEventExecutor(
            Func<TEvent, Guid> getSagaId,
            Action<TEvent, TSaga> method)
        {
            _getSagaId = getSagaId;
            _method = method;
        }

        public void Transition(TEvent @event)
        {
            var arFactory = NcqrsEnvironment.Get<IAggregateRootCreationStrategy>();

            var sagaId = _getSagaId(@event);

            dynamic saga = null;

            Action<IUnitOfWorkContext> uowAction = 
                uow => saga = uow.GetById<TSaga>(sagaId) ?? arFactory.CreateAggregateRoot<TSaga>();

            if (UnitOfWorkContext.Current != null)
            {
                uowAction(UnitOfWorkContext.Current);
            } 
            else
            {
                using (var uow = new UnitOfWorkFactory().CreateUnitOfWork(Guid.NewGuid()))
                    uowAction(uow);
            }
            
            saga.HandleEvent(@event);

            StoreEvent(@event, sagaId, saga);
        }


        private void StoreEvent(TEvent @event, Guid sagaId, TSaga saga)
        {

            var store = NcqrsEnvironment.Get<IEventStore>();
            var idGenerator = NcqrsEnvironment.Get<IUniqueIdentifierGenerator>();
            var clock = NcqrsEnvironment.Get<IClock>();

            var e = new UncommittedEvent(
                idGenerator.GenerateNewId(),
                sagaId,
                saga.InitialVersion + 1,
                saga.InitialVersion,
                clock.UtcNow(),
                @event,
                null);

            var stream = new UncommittedEventStream(idGenerator.GenerateNewId());
            stream.Append(e);

            store.Store(stream);

            saga.AcceptChanges();

        }

    }
}
