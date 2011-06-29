using System;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Eventing.Sourcing.Snapshotting;

namespace Ncqrs.Saga
{

    public interface ISaga 
    {

        Guid EventSourceId { get; }
        long InitialVersion { get; }

        event EventHandler<CommandDispatchedEventArgs> CommandDispatched;
        event EventHandler<EventAppliedEventArgs> EventApplied;
        void InitializeFromHistory(CommittedEventStream history);
        void InitializeFromSnapshot(Snapshot snapshot);

    }

}
