using Ncqrs.Eventing.ServiceModel.Bus;

namespace Ncqrs.Saga.Mapping
{

    public interface ISagaMapping
    {

        void RegisterMappings(InProcessEventBus eventBus);

    }

}
