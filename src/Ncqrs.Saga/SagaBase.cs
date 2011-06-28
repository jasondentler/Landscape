using System;
using System.Collections.Generic;
using System.Threading;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Eventing.Storage;

namespace Ncqrs.Saga
{

    public abstract class SagaBase : EventSource, ISaga
    {

        private static readonly ThreadLocal<List<Action<ISaga, ICommand>>> CommandDispatchedCallbacks =
            new ThreadLocal<List<Action<ISaga, ICommand>>>(() => new List<Action<ISaga, ICommand>>());

        public static void RegisterThreadStaticCommandDispatchCallback(Action<ISaga, ICommand> callback)
        {
            CommandDispatchedCallbacks.Value.Add(callback);
        }

        public static void UnregisterThreadStaticCommandDispatchCallback(Action<ISaga, ICommand> callback)
        {
            CommandDispatchedCallbacks.Value.Remove(callback);
        }

        internal void Handle<TEvent>(
            Guid commitId,
            IPublishableEvent @event,
            Action<TEvent> handler,
            IEventStore eventStore,
            ICommandService commandService)
        {

            IEnumerable<ICommand> dispatches;
            using (var ctx = new SagaDispatchContext())
            {
                handler((TEvent) @event.Payload);
                dispatches = ctx.Dispatches;
            }

            var uncommittedEvent = new UncommittedEvent(
                @event.EventIdentifier,
                EventSourceId,
                0,
                InitialVersion,
                @event.EventTimeStamp,
                @event.Payload,
                @event.EventVersion);

            var stream = new UncommittedEventStream(commitId);
            stream.Append(uncommittedEvent);
            eventStore.Store(stream);

            foreach (var dispatch in dispatches)
                commandService.Execute(dispatch);
        }

        protected void Dispatch(ICommand command)
        {
            OnCommandDispatched(command);
        }

        protected virtual void OnCommandDispatched(ICommand command)
        {
            if (command != null)
                CommandDispatched(this, new CommandDispatchedEventArgs(command));

            var callbacks = CommandDispatchedCallbacks.Value;

            foreach (var callback in callbacks)
                callback(this, command);
        }

        public event EventHandler<CommandDispatchedEventArgs> CommandDispatched;

    }
}
