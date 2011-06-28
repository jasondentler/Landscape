using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding;

namespace Ncqrs.Saga
{
    public class SagaDispatchContext : IDisposable
    {

        [ThreadStatic] private static SagaDispatchContext _threadInstance;

        private readonly List<ICommand> _dispatches = new List<ICommand>();
        private Action<ISaga, ICommand> _commandDispatchedCallback;

        public IEnumerable<ICommand> Dispatches
        {
            get { return _dispatches; }
        }

        public static SagaDispatchContext Current
        {
            get { return _threadInstance; }
        }

        public bool IsDisposed { get; private set; }

        public SagaDispatchContext()
        {
            _threadInstance = this;
            IsDisposed = false;
            InitializeCommandDispatchedHandler();
        }

        private void InitializeCommandDispatchedHandler()
        {
            if (_commandDispatchedCallback == null)
                _commandDispatchedCallback = new Action<ISaga, ICommand>(CommandDispatchedHandler);

            SagaBase.RegisterThreadStaticCommandDispatchCallback(_commandDispatchedCallback);
        }

        private void DestroyCommandDispatchedHandler()
        {
            if (_commandDispatchedCallback != null)
                SagaBase.UnregisterThreadStaticCommandDispatchCallback(_commandDispatchedCallback);
        }

        private void CommandDispatchedHandler(ISaga saga, ICommand command)
        {
            _dispatches.Add(command);
        }


        ~SagaDispatchContext()
        {
            Dispose(false);
        }

        public void Dispose()
        {
        }

        private void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    DestroyCommandDispatchedHandler();
                    _threadInstance = null;
                }
                IsDisposed = true;
            }
        }

    }
}
