using System;
using System.Collections.Generic;
using System.Threading;
using Ncqrs.Commanding;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Eventing.Sourcing.Snapshotting;

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

        protected SagaBase()
        {
        }

        protected SagaBase(Guid sagaSagaId)
            :base(sagaSagaId)
        {
        }

        protected override void OnEventApplied(Eventing.UncommittedEvent evnt)
        {
            if (EventApplied != null)
                EventApplied(this, new EventAppliedEventArgs(evnt));
        }

        public event EventHandler<CommandDispatchedEventArgs> CommandDispatched;
        public event EventHandler<EventAppliedEventArgs> EventApplied;

        protected void Dispatch(ICommand command)
        {
            OnCommandDispatched(command);
        }

        protected virtual void OnCommandDispatched(ICommand command)
        {
            if (command != null && CommandDispatched != null)
                CommandDispatched(this, new CommandDispatchedEventArgs(command));

            var callbacks = CommandDispatchedCallbacks.Value;

            foreach (var callback in callbacks)
                callback(this, command);
        }
        
    }
}
