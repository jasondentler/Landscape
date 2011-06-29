using System;
using Ncqrs.Domain;

namespace Restbucks.Barista
{
    public class Order : AggregateRootMappedByConvention 
    {

        private enum State
        {
            Queued,
            BeingPrepared,
            Prepared
        }

        private State _state;
        private Guid _deliverySagaId;

        private Order()
        {
        }

        public Order(
            Guid orderId,
            Location location,
            OrderItemInfo[] items,
            Guid deliverySagaId)
            : base(orderId)
        {
            var e = new OrderQueued(
                orderId,
                location,
                items,
                deliverySagaId);
            ApplyEvent(e);
        }

        public void BeginPreparingOrder()
        {
            if (_state == State.BeingPrepared)
                throw new InvalidAggregateStateException("This order is already being prepared.");

            if (_state == State.Prepared)
                throw new InvalidAggregateStateException("This order is already prepared."); 

            var e = new OrderBeingPrepared(EventSourceId);
            ApplyEvent(e);
        }

        public void FinishPreparingOrder()
        {
            if (_state == State.Queued)
                throw new InvalidAggregateStateException("You never started preparing this order.");

            var e = new OrderPrepared(EventSourceId, _deliverySagaId);
            ApplyEvent(e);
        }

        protected void On(OrderQueued e)
        {
            _state = State.Queued;
            _deliverySagaId = e.DeliverySagaId;
        }

        protected void On(OrderBeingPrepared e)
        {
            _state = State.BeingPrepared;
        }

        protected void On(OrderPrepared e)
        {
            _state = State.Prepared;
        }

    }
}
