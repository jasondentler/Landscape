using System;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Ncqrs.Saga.Mapping
{
    public static class SagaEventExecutorExtensions
    {


        public static void RegisterWith<TEvent, TSaga>(
            this ISagaEventExecutor<TEvent, TSaga> executor,  
            InProcessEventBus eventBus)
            where TSaga : class, ISaga
        {
            eventBus.RegisterHandler(typeof (TEvent),
                                     pe =>
                                         {
                                             var e = (TEvent) pe.Payload;
                                             Console.WriteLine("Sending {0} to {1}", typeof (TEvent), typeof (TSaga));
                                             executor.Transition(e);
                                         });
        }

    }
}
