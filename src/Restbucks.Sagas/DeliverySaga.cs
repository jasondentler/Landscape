using System;
using Ncqrs.Saga;
using Stateless;

namespace Restbucks.Sagas
{
    public class DeliverySaga : SagaMappedByConvention 
    {

        public enum State 
        {
            Initial,
            Paid,
            Prepared,
            Delivered
        }

        public enum Trigger
        {
            Paid,
            Prepared
        }

        private Guid _baristaOrderId;
        private StateMachine<State, Trigger> _stateMachine;

        private DeliverySaga()
        {
            InitializeStateMachine();
        }

        public DeliverySaga(Guid sagaId)
            :base(sagaId)
        {
            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {
            _stateMachine = new StateMachine<State, Trigger>(State.Initial);

            _stateMachine.Configure(State.Initial)
                .Permit(Trigger.Paid, State.Paid)
                .Permit(Trigger.Prepared, State.Prepared);

            _stateMachine.Configure(State.Paid)
                .Permit(Trigger.Prepared, State.Delivered);
            _stateMachine.Configure(State.Prepared)
                .Permit(Trigger.Paid, State.Delivered);
        }

        protected override void OnInitialized()
        {
            _stateMachine.Configure(State.Delivered)
                .OnEntry(Deliver);
        }

        public void Paid(Billing.OrderPaid e)
        {
            if (_stateMachine.CanFire(Trigger.Paid))
                ApplyEvent(e);
        }

        public void Prepared(Barista.OrderPrepared e)
        {
            if (_stateMachine.CanFire(Trigger.Prepared))
                ApplyEvent(e);
        }

        protected void On(Billing.OrderPaid e)
        {
            _stateMachine.Fire(Trigger.Paid);
        }

        protected void On(Barista.OrderPrepared e)
        {
            _baristaOrderId = e.OrderId;
            _stateMachine.Fire(Trigger.Prepared);
        }

        public void Deliver()
        {
            var cmd = new Barista.DeliverOrder(_baristaOrderId);
            Dispatch(cmd);
        }
    }
}
