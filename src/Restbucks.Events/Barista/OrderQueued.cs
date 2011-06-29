using System;

namespace Restbucks.Barista
{

    public class OrderQueued : IEvent 
    {

        public Guid OrderId { get; private set; }
        public Location Location { get; private set; }
        public OrderItemInfo[] Items { get; private set; }
        public Guid DeliverySagaId { get; set; }

        public OrderQueued(Guid orderId,
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
