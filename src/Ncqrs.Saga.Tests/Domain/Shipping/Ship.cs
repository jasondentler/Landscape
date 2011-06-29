using System;
using Ncqrs.Commanding;

namespace Ncqrs.Saga.Domain.Shipping
{
    public class Ship : CommandBase 
    {
        public Guid ShipmentId { get; set; }

        public Ship(Guid shipmentId)
        {
            ShipmentId = shipmentId;
        }
    }
}
