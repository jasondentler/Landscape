using System;
using Ncqrs.Saga.Domain.Billing;
using Ncqrs.Saga.Domain.Shipping;
using Stateless;

namespace Ncqrs.Saga.Sagas
{

    public class ShippingSaga : SagaBase
    {

        public enum State
        {
            Initial,
            Paid,
            Prepared,
            Shipped
        }


        public enum Trigger
        {
            Paid,
            Prepared
        }

        private Guid _shipmentId;

        private StateMachine<State, Trigger> _stateMachine;

        private ShippingSaga()
        {
            InitializeStateMachine();
        }

        public ShippingSaga(Guid shippingSagaId)
            : base(shippingSagaId)
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
                .Permit(Trigger.Prepared, State.Shipped);
            _stateMachine.Configure(State.Prepared)
                .Permit(Trigger.Paid, State.Shipped);
        }

        public override void OnInitialized()
        {
            _stateMachine.Configure(State.Shipped)
                .OnEntry(Ship);
        }

        public void InvoicePaid(InvoicePaid e)
        {
            if (_stateMachine.CanFire(Trigger.Paid))
                ApplyEvent(e);
        }

        public void ShipmentPrepared(ShipmentPrepared e)
        {
            if (_stateMachine.CanFire(Trigger.Prepared))
                ApplyEvent(e);
        }

        protected void On(InvoicePaid e)
        {
            _stateMachine.Fire(Trigger.Paid);
        }

        protected void On(ShipmentPrepared e)
        {
            _shipmentId = e.ShipmentId;
            _stateMachine.Fire(Trigger.Prepared);
        }

        private void Ship()
        {
            var cmd = new Ship(_shipmentId);
            Dispatch(cmd);
        }

    }

}
