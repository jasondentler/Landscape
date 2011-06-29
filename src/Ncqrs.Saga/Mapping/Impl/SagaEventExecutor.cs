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
        private readonly Func<Guid, TSaga> _constructor;
        private readonly Action<TEvent, TSaga> _method;

        public SagaEventExecutor(
            Func<TEvent, Guid> getSagaId,
            Func<Guid, TSaga> constructor,
            Action<TEvent, TSaga> method)
        {
            _getSagaId = getSagaId;
            _constructor = constructor;
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

            TSaga saga = null;

            Action<IUnitOfWorkContext> uowAction =
                uow => saga = uow.GetById<TSaga>(sagaId) ?? _constructor(sagaId);

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
            var stream = new UncommittedEventStream(idGenerator.GenerateNewId());

            foreach (var @event in events)
                stream.Append(@event);

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
