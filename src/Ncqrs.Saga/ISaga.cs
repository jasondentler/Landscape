using System;
using System.Collections.Generic;
using Ncqrs.Eventing.Sourcing;

namespace Ncqrs.Saga
{

    public interface ISaga : IEventSource 
    {

        event EventHandler<CommandDispatchedEventArgs> CommandDispatched;
        
    }

}
