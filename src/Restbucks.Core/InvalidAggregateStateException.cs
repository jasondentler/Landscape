using System;

namespace Restbucks
{
    public class InvalidAggregateStateException : ApplicationException
    {

        public InvalidAggregateStateException(string message)
            : base(message)
        {
        }

        public InvalidAggregateStateException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

    }
}
