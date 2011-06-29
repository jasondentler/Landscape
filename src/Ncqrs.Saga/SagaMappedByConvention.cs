using System;
using Ncqrs.Eventing.Sourcing.Mapping;

namespace Ncqrs.Saga
{
    public abstract class SagaMappedByConvention : MappedSaga
    {

        protected SagaMappedByConvention()
            : base(new ConventionBasedEventHandlerMappingStrategy())
        {
        }

        protected SagaMappedByConvention(Guid sagaId)
            : base(sagaId, new ConventionBasedEventHandlerMappingStrategy())
        {
        }


    }


}
