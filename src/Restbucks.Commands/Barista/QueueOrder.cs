using System;
using Ncqrs.Commanding;

namespace Restbucks.Barista
{
    public class QueueOrder : CommandBase 
    {
        public Guid OrderId { get; private set; }
        public Location Location { get; private set; }
        public OrderItemInfo[] Items { get; private set; }
        public Guid DeliverySagaId { get; private set; }

        public QueueOrder(
            Guid orderId,
            Location location,
            OrderItemInfo[] items,
            Guid deliverySagaId)
        {
            OrderId = orderId;
            Location = location;
            Items = items;
            DeliverySagaId = deliverySagaId;
        }
    }
}
