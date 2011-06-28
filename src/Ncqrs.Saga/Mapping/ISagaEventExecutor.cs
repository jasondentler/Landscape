using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ncqrs.Saga.Mapping
{
    public interface ISagaEventExecutor<TEvent, TSaga>
        where TSaga : class, ISaga
    {

        void Transition(TEvent @event);
        
    }
}
