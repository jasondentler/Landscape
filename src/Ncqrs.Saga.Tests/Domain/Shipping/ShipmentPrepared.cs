using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ncqrs.Saga.Domain.Shipping
{
    public class ShipmentPrepared
    {
        public Guid ShipmentId { get; private set; }
        public Guid ShippingSagaId { get; private set; }

        public ShipmentPrepared(Guid shipmentId, Guid shippingSagaId)
        {
            ShipmentId = shipmentId;
            ShippingSagaId = shippingSagaId;
        }
    }
}
