using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Ncqrs.Saga
{

    public class SagaHandler
    {
        
        private static readonly ConcurrentDictionary<Type, Type> _wrapperToEventMap = new ConcurrentDictionary<Type, Type>();

        private static readonly ConcurrentDictionary<Type, ISet<SagaRegistration>> _registrations =
            new ConcurrentDictionary<Type, ISet<SagaRegistration>>();
        
        internal static void Register<TEvent, TSaga>(
            Func<IPublishedEvent<TEvent>, TSaga> loadSaga,
            Action<TSaga, IPublishedEvent<TEvent>> handler)
            where TSaga : ISaga
        {
            Func<IPublishableEvent, ISaga> wrappedLoadSaga = @event => loadSaga((IPublishedEvent<TEvent>) @event);
            Action<ISaga, IPublishableEvent> wrappedHandler =
                (saga, @event) => handler((TSaga) saga, (IPublishedEvent<TEvent>) @event);

            var registration = new SagaRegistration(wrappedLoadSaga, wrappedHandler);

            _registrations.AddOrUpdate(typeof (TEvent),
                                       t => new HashSet<SagaRegistration>(new[] {registration}),
                                       (t, registrations) =>
                                           {
                                               lock (registrations)
                                               {
                                                   registrations.Add(registration);
                                                   return registrations;
                                               }
                                           });
        }

        public void Handle(IPublishableEvent @event)
        {
            if (null == @event)
                throw new NullReferenceException("@event");

            var eventType = GetEventType(@event.GetType());

            var registrations = GetSagaRegistrations(eventType);

            foreach (var registration in registrations)
                Handle(registration, @event);
        }

        private Type GetEventType(Type eventType)
        {
            Type result;
            if (_wrapperToEventMap.TryGetValue(eventType, out result))
                return result;

            var interfaces = eventType.GetInterfaces();
            var matchedInterface = interfaces.Where(i => _wrapperToEventMap.ContainsKey(i)).FirstOrDefault();

            if (matchedInterface != null && _wrapperToEventMap.TryGetValue(matchedInterface, out result))
            {
                _wrapperToEventMap[eventType] = result;
                return result;
            }

            var iface = eventType.GetInterfaces()
                .Where(i => i.ContainsGenericParameters)
                .Where(i => i.GetGenericTypeDefinition() ==  typeof (IPublishedEvent<>))
                .FirstOrDefault();

            // iface must be IPublishedEvent<TEvent>
            if (iface != null)
            {
                result = iface.GetGenericArguments()[0];
                _wrapperToEventMap[eventType] = result;
                _wrapperToEventMap[iface] = result;
            }

            throw new NotSupportedException(string.Format("{0} doesn't implement IPublishedEvent<TEvent>.", eventType));

        }

        private SagaRegistration[] GetSagaRegistrations(Type eventType)
        {
            ISet<SagaRegistration> registrations;
            if (!_registrations.TryGetValue(eventType, out registrations))
                return new SagaRegistration[0];
            lock (registrations)
            {
                return registrations.ToArray();
            }
        }

        private void Handle(SagaRegistration registration, IPublishableEvent @event)
        {
            var saga = registration.LoadSaga(@event);
            registration.Handler(saga, @event);
        }

    }

}
