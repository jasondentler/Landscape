using System;
using System.Collections.Generic;
using Ncqrs.Domain;

namespace Restbucks.Ordering
{

    public class Order : AggregateRootMappedByConvention 
    {

        private readonly HashSet<OrderItem> _items = new HashSet<OrderItem>();
        private OrderState _state = OrderState.Created;
        private Location _location;

        private Order()
        {
        }

        public Order(
            Guid orderId,
            Guid orderItemId, 
            Guid productId, 
            IDictionary<string, string> prefernces,
            int quantity)
            : base(orderId)
        {
            var e = new OrderCreated(EventSourceId);
            ApplyEvent(e);

            AddItem(orderItemId, productId, prefernces, quantity);
        }

        public void AddItem(
            Guid orderItemId, 
            Guid productId,
            IDictionary<string, string> preferences,
            int quantity)
        {
            _items.Add(new OrderItem(this, orderItemId, productId, preferences, quantity));
        }

        public void PlaceOrder(Location location)
        {
            var e = new OrderPlaced(EventSourceId, location);
            ApplyEvent(e);
        }

        public void ChangeLocation(Location newLocation)
        {

            if (_state == OrderState.Created)
                throw new InvalidAggregateStateException("You can't change the order location before you place the order.");

            if (newLocation == _location) 
                return;

            var e = new OrderLocationChanged(EventSourceId, _location, newLocation);
            ApplyEvent(e);
        }


        protected void On(OrderCreated e)
        {
        }

        protected void On(OrderItemAdded e)
        {
        }

        protected void On(OrderPlaced e)
        {
            _state = OrderState.Placed;
            _location = e.Location;
        }

        protected void On(OrderLocationChanged e)
        {
            _location = e.Location;
        }

    }

}
