using System;
using System.Collections.Generic;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Storage;
using Ncqrs.Spec;

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

            var sagaId = _getSagaId(@event);
            dynamic saga = LoadSaga(sagaId);

            IEnumerable<ICommand> dispatches;
            IEnumerable<UncommittedEvent> events;
            using (var eventContext = new EventContext())
            using (var dispatchContext = new SagaDispatchContext())
            {
                _method(@event, saga);
                dispatches = dispatchContext.Dispatches;
                events = eventContext.Events;
            }

            StoreEvent(events, sagaId, saga);

            DispatchCommands(dispatches);

        }

        private TSaga LoadSaga(Guid sagaId)
        {
            var arFactory = NcqrsEnvironment.Get<IAggregateRootCreationStrategy>();

            TSaga saga = null;

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

            return saga;

        }

        private void StoreEvent(IEnumerable<UncommittedEvent> events, Guid sagaId, TSaga saga)
        {

            var store = NcqrsEnvironment.Get<IEventStore>();
            var idGenerator = NcqrsEnvironment.Get<IUniqueIdentifierGenerator>();
            var clock = NcqrsEnvironment.Get<IClock>();
            var stream = new UncommittedEventStream(idGenerator.GenerateNewId());

            foreach (var @event in events)
            {
                var e = new UncommittedEvent(
                    idGenerator.GenerateNewId(),
                    sagaId,
                    saga.InitialVersion + 1,
                    saga.InitialVersion,
                    clock.UtcNow(),
                    @event.Payload,
                    null);

                stream.Append(e);
            }

            store.Store(stream);

            saga.AcceptChanges();

        }

        private void DispatchCommands(IEnumerable<ICommand> dispatches)
        {
            var commandService = NcqrsEnvironment.Get<ICommandService>();
            foreach (var cmd in dispatches)
                commandService.Execute(cmd);
        }

    }
}
