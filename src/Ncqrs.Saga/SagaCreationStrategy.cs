using System;
using System.Reflection;

namespace Ncqrs.Saga
{
    public class SagaCreationStrategy : ISagaCreationStrategy
    {
        public TSaga CreateSaga<TSaga>()
        {
            var sagaType = typeof (TSaga);

            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            var ctor = sagaType.GetConstructor(flags, null, Type.EmptyTypes, null);

            if (ctor == null)
            {
                var message = string.Format("No constructor found on saga root type {0} that accepts no parameters.",
                                            sagaType.AssemblyQualifiedName);
                throw new NotSupportedException(message);
            }

            var saga = (TSaga) ctor.Invoke(null);
            return saga;
        }
    }
}
