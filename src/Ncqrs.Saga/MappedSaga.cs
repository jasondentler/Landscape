using System;
using Ncqrs.Eventing.Sourcing.Mapping;

namespace Ncqrs.Saga
{
    public abstract class MappedSaga : SagaBase
    {
        [NonSerialized]
        private readonly IEventHandlerMappingStrategy _mappingStrategy;

        protected MappedSaga(IEventHandlerMappingStrategy mappingStrategy)
        {
            _mappingStrategy = mappingStrategy;
            InitializeHandlers();
        }

        protected MappedSaga(Guid sagaSagaId, IEventHandlerMappingStrategy mappingStrategy)
            : base(sagaSagaId)
        {
            _mappingStrategy = mappingStrategy;
            InitializeHandlers();
        }

        protected void InitializeHandlers()
        {
            foreach (var handler in _mappingStrategy.GetEventHandlers(this))
                RegisterHandler(handler);
        }
    }
}
