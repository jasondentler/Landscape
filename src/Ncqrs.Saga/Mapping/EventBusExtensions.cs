using System;
using System.Linq;
using System.Reflection;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Ncqrs.Saga.Mapping
{
    public static class EventBusExtensions
    {

        public static void RegisterSagaMappingsIn(this InProcessEventBus bus, Assembly asm)
        {

            var mappingTypes = asm.GetTypes()
                .Where(t => t.IsClass &&
                            !t.IsAbstract &&
                            typeof (ISagaMapping).IsAssignableFrom(t));

            foreach (var mappingType in mappingTypes)
            {
                var mapping = (ISagaMapping) Activator.CreateInstance(mappingType);
                mapping.RegisterMappings(bus);
            }



        }


    }
}
