using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Domain;

namespace Ncqrs.Saga.Mapping.Impl
{
    public class MappedSagaEvent<TEvent, TSaga>
        : IMappedSagaEvent<TEvent, TSaga>,
        IMappedEventToSaga<TEvent, TSaga> 
        where TSaga : AggregateRoot, ISaga
    {

        private Func<TEvent, Guid> _getSagaId;
        private Action<TEvent, TSaga> _method;

        public IMappedEventToSaga<TEvent, TSaga> WithId(Func<TEvent, Guid> getSagaId)
        {
            _getSagaId = getSagaId;
            return this;
        }

        public ISagaEventExecutor<TEvent, TSaga> ToCallOn(Action<TEvent, TSaga> method)
        {
            _method = method;
            return GenerateExecutor();
        }

        private ISagaEventExecutor<TEvent, TSaga> GenerateExecutor()
        {
            return new SagaEventExecutor<TEvent, TSaga>(
                _getSagaId,
                _method);
        }

    }
}
