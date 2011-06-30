using System;
using System.Collections.Generic;
using Ncqrs.Domain;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Eventing.Sourcing.Snapshotting;

namespace Ncqrs.Saga
{
    public abstract class EventSource
    {

        [NonSerialized]
        private readonly IUniqueIdentifierGenerator _idGenerator;

        [NonSerialized]
        private readonly List<ISourcedEventHandler> _eventHandlers = new List<ISourcedEventHandler>();


        private Guid _eventSourceId;
        private long _initialVersion;
        private long _currentVersion;

        protected EventSource()
        {
            _idGenerator = NcqrsEnvironment.Get<IUniqueIdentifierGenerator>();
            _eventSourceId = _idGenerator.GenerateNewId();
        }

        protected EventSource(Guid eventSourceId)
            : this()
        {
            _eventSourceId = eventSourceId;
        }
        
        public Guid EventSourceId { get { return _eventSourceId; } }
        public long InitialVersion { get { return _initialVersion; } }

        internal protected void RegisterHandler(ISourcedEventHandler handler)
        {
            _eventHandlers.Add(handler);
        }

        public void InitializeFromSnapshot(Snapshot snapshot)
        {
            Initialize(snapshot);
            OnInitialized();
        }

        public void InitializeFromHistory(CommittedEventStream history)
        {
            Initialize(history);
            OnInitialized();
        }

        protected virtual void Initialize(Snapshot snapshot)
        {
            _eventSourceId = snapshot.EventSourceId;
            _initialVersion = _currentVersion = snapshot.Version;
        }

        protected virtual void Initialize(CommittedEventStream history)
        {
            if (_initialVersion != _currentVersion)
                throw new InvalidOperationException("Can't apply history when instance has uncommitted changed.");

            if (history.IsEmpty)
                return;

            _eventSourceId = history.SourceId;

            foreach (var historicalEvent in history)
                ApplyEventFromHistory(historicalEvent);

            _initialVersion = history.CurrentSourceVersion;
        }

        protected virtual void OnInitialized()
        {
        }

        protected abstract void OnEventApplied(UncommittedEvent evnt);

        protected virtual void HandleEvent(object evnt)
        {
            bool handled = false;
            var handlers = new List<ISourcedEventHandler>(_eventHandlers);

            foreach (var handler in handlers)
                handled |= handler.HandleEvent(evnt);

            if (!handled)
                throw new EventNotHandledException(evnt);

        }

        internal protected void ApplyEvent(object evnt)
        {
            var eventVersion = evnt.GetType().Assembly.GetName().Version;
            var eventSequence = GetNextSequence();
            var wrappedEvent = new UncommittedEvent(
                _idGenerator.GenerateNewId(),
                EventSourceId,
                eventSequence,
                _initialVersion,
                DateTime.UtcNow,
                evnt,
                eventVersion);
            HandleEvent(wrappedEvent.Payload);
            OnEventApplied(wrappedEvent);
        }

        private long GetNextSequence()
        {
            if (_initialVersion > 0 && _currentVersion == 0)
                _currentVersion = _initialVersion;
            _currentVersion++;
            return _currentVersion;
        }

        private void ApplyEventFromHistory(CommittedEvent evnt)
        {
            HandleEvent(evnt.Payload);
            _currentVersion++;
        }

        public void AcceptChanges()
        {
            _initialVersion = _currentVersion;
        }

        public override string ToString()
        {
            return string.Format("{0}[{1}]", GetType().FullName, EventSourceId.ToString("D"));
        }


    }
}
