using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Sourcing;
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
            TSaga saga = LoadSaga(sagaId);

            var dispatches = new List<ICommand>();
            var dispatchHandlerDelegate = GetDispatchHandler(dispatches);

            var events = new List<UncommittedEvent>();
            var eventHandlerDelegate = GetEventHandler(events);

            saga.EventApplied += eventHandlerDelegate;
            saga.CommandDispatched += dispatchHandlerDelegate;
            try
            {
                _method(@event, saga);
            }
            finally
            {
                saga.EventApplied -= eventHandlerDelegate;
                saga.CommandDispatched -= dispatchHandlerDelegate;
            }

            StoreEvent(events, saga);
            DispatchCommands(dispatches);

        }

        private TSaga LoadSaga(Guid sagaId)
        {
            TSaga saga;
            var store = NcqrsEnvironment.Get<IEventStore>();
            var creationStrategy = NcqrsEnvironment.Get<ISagaCreationStrategy>();
            var stream = store.ReadFrom(sagaId, 0, long.MaxValue);

            if (stream.IsEmpty)
            {
                saga = _constructor(sagaId);
                if (saga.EventSourceId != sagaId)
                {
                    var msg = string.Format("The {0} returned by the mapped constructor has the wrong EventSouceId",
                                            typeof (TSaga));
                    throw new AggregateRootCreationException(msg);
                }
                return saga;
            }

            saga = creationStrategy.CreateSaga<TSaga>();
            saga.InitializeFromHistory(stream);
            return saga;
        }

        private void StoreEvent(IEnumerable<UncommittedEvent> events, TSaga saga)
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

        private EventHandler<EventAppliedEventArgs> GetEventHandler(IList<UncommittedEvent> events)
        {
            Action<object, EventAppliedEventArgs> eventHandler = null;
            eventHandler = (sender, args) => events.Add(args.Event);
            return new EventHandler<EventAppliedEventArgs>(eventHandler);
        }

        private EventHandler<CommandDispatchedEventArgs> GetDispatchHandler(IList<ICommand> dispatches)
        {
            Action<object, CommandDispatchedEventArgs> dispatchHandler = null;
            dispatchHandler = (sender, args) => dispatches.Add(args.Command);
            return new EventHandler<CommandDispatchedEventArgs>(dispatchHandler);
        }


    }
}
