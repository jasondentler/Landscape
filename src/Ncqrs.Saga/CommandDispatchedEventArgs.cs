using System;
using Ncqrs.Commanding;

namespace Ncqrs.Saga
{
    public class CommandDispatchedEventArgs : EventArgs
    {

        private readonly ICommand _command;

        public CommandDispatchedEventArgs(ICommand command)
        {
            _command = command;
        }

        public ICommand Command
        {
            get { return _command; }
        }

    }
}
