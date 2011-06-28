using System;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Ncqrs.Saga
{
    public class SagaRegistration
    {
        public Func<IPublishableEvent, ISaga> LoadSaga { get; private set; }
        public Action<ISaga, IPublishableEvent> Handler { get; private set; }

        public SagaRegistration(
            Func<IPublishableEvent, ISaga> loadSaga,
            Action<ISaga, IPublishableEvent> handler)
        {
            LoadSaga = loadSaga;
            Handler = handler;
        }
    }
}