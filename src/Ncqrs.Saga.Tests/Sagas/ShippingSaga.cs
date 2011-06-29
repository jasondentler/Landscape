using System;
using Ncqrs.Saga.Domain.Billing;
using Ncqrs.Saga.Domain.Shipping;

namespace Ncqrs.Saga.Sagas
{

    public class ShippingSaga : SagaBase 
    {

        [Flags]
        private enum State
        {
            None = 0,
            InvoicePaid = 1,
            ShipmentPrepared = 2,
            ReadyToShip = 3,
            Shipped = 4
        }

        private Guid _shipmentId;

        private State _state;

        private ShippingSaga()
        {
        }

        public ShippingSaga(Guid shippingSagaId)
            : base(shippingSagaId)
        {
        }

        public void OrderPlaced()
        {
        }

        protected void On(InvoicePaid e)
        {
            _state = _state | State.InvoicePaid;
            if (_state == State.ReadyToShip)
                Ship();
        }

        protected void On(ShipmentPrepared e)
        {
            _shipmentId = _shipmentId;
            _state = _state | State.ShipmentPrepared;
            if (_state == State.ReadyToShip)
                Ship();
        }

        private void Ship()
        {
            var cmd = new Ship(_shipmentId);
            Dispatch(cmd);
            _state = State.Shipped;
        }

    }

}
