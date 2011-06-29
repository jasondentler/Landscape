using System;
using Ncqrs.Domain;

namespace Ncqrs.Saga.Mapping
{
    public interface IMappedEventToSaga<TEvent, TSaga>
        where TSaga : class, ISaga
    {

        IMappedEventToSagaWithConstructor<TEvent, TSaga> OrCreate(Func<Guid, TSaga> constructor);


    }
}
