using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;

namespace Restbucks
{

    /// <summary>
    /// Allows cross-BC event handlers to push commands without colliding units of work.
    /// </summary>
    /// <remarks>>
    /// This forces everything to execute syncronously to make our domain easy to test.
    /// If, while handling an event, we dump a command in to the regular command service directly,
    /// we'd get an error creating a second unit of work before we finished publishing events from the first.
    /// Any async event bus would easily avoid this problem, but make our domain hard to test.
    /// </remarks>
    public class QueuedCommandService : ICommandService
    {

        private readonly CommandService _actualCommandService;
        private readonly ConcurrentQueue<ICommand> _commandQueue;
        private readonly object _sync = new object();
        private bool _isExecuting = false;

        public QueuedCommandService(CommandService actualCommandService)
        {
            _actualCommandService = actualCommandService;
            _commandQueue = new ConcurrentQueue<ICommand>();
        }

        public void Execute(ICommand command)
        {
            _commandQueue.Enqueue(command);
            ExecuteUntilEmpty();
        }

        private void ExecuteUntilEmpty()
        {
            if (!_isExecuting)
            lock (_sync)
            {
                if (!_isExecuting)
                {
                    _isExecuting = true;
                    try
                    {
                        SafeExecuteUntilEmpty();
                    }
                    finally
                    {
                        // In case a test threw an exception, clear any remaining queued commands
                        ClearQueue();
                        _isExecuting = false;
                    }
                }
            }
        }

        private void SafeExecuteUntilEmpty()
        {
            ICommand command;
            while (_commandQueue.TryDequeue(out command))
                _actualCommandService.Execute(command);
        }

        private void ClearQueue()
        {
            ICommand command;
            while (_commandQueue.TryDequeue(out command))
            {
            }
        }

    }

}
