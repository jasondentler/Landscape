using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ncqrs.Saga.Mapping
{
    public interface IMappedEvent<TEvent>
    {

        IMappedSagaEvent<TEvent, TSaga> ToSaga<TSaga>()
            where TSaga : class, ISaga;

    }
}
