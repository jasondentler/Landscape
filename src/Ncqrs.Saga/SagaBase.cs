using System;
using System.Collections.Generic;
using System.Threading;
using Ncqrs.Commanding;
using Ncqrs.Domain;
using Ncqrs.Eventing;

namespace Ncqrs.Saga
{

    public abstract class SagaBase : AggregateRootMappedByConvention, ISaga
    {

        protected SagaBase()
        {
        }

        protected SagaBase(Guid sagaId)
            : base(sagaId)
        {
        }

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

        public event EventHandler<CommandDispatchedEventArgs> CommandDispatched;

        public override void InitializeFromHistory(CommittedEventStream history)
        {
            base.InitializeFromHistory(history);
            OnInitialized();
        }

        public override void InitializeFromSnapshot(Eventing.Sourcing.Snapshotting.Snapshot snapshot)
        {
            base.InitializeFromSnapshot(snapshot);
            OnInitialized();
        }

        public virtual void OnInitialized()
        {
        }

    }
}
