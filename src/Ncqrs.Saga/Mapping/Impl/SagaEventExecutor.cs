using System;
using System.Collections.Generic;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Storage;

namespace Ncqrs.Saga.Mapping.Impl
{
    public class SagaEventExecutor<TEvent, TSaga>
        : ISagaEventExecutor<TEvent, TSaga>
        where TSaga : class, ISaga
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


            _method(@event, saga);
            dispatches = new ICommand[0];
            events = new UncommittedEvent[0];


            StoreEvent(events, sagaId, saga);

            DispatchCommands(dispatches);

        }

        private TSaga LoadSaga(Guid sagaId)
        {
            var store = NcqrsEnvironment.Get<IEventStore>();
            var creationStrategy = NcqrsEnvironment.Get<ISagaCreationStrategy>();
            var stream = store.ReadFrom(sagaId, 0, long.MaxValue);

            var saga = creationStrategy.CreateSaga<TSaga>();
            saga.InitializeFromHistory(stream);

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

        }

        private void DispatchCommands(IEnumerable<ICommand> dispatches)
        {
            var commandService = NcqrsEnvironment.Get<ICommandService>();
            foreach (var cmd in dispatches)
                commandService.Execute(cmd);
        }

    }
}
