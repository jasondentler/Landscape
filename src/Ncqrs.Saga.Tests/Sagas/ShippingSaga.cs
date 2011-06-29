using System;
using Ncqrs.Saga.Domain.Billing;
using Ncqrs.Saga.Domain.Shipping;

namespace Ncqrs.Saga.Sagas
{

    public class ShippingSaga : SagaBase
    {

        private bool _isPaid;
        private bool _isPrepared;

        private Guid _shipmentId;

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

        public void InvoicePaid(InvoicePaid e)
        {
            if (_isPaid)
                return;
            ApplyEvent(e);
            if (CanShip()) Ship();
        }

        public void ShipmentPrepared(ShipmentPrepared e)
        {
            if (_isPrepared)
                return;
            ApplyEvent(e);
            if (CanShip()) Ship();
        }

        protected void On(InvoicePaid e)
        {
            _isPaid = true;
        }

        protected void On(ShipmentPrepared e)
        {
            _shipmentId = e.ShipmentId;
            _isPrepared = true;
        }

        private bool CanShip()
        {
            return _isPaid && _isPrepared;
        }

        private void Ship()
        {
            var cmd = new Ship(_shipmentId);
            Dispatch(cmd);
        }

    }

}
