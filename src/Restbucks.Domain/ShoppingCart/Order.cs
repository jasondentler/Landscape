using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs.Domain;

namespace Restbucks.ShoppingCart
{

    public class Order : AggregateRootMappedByConvention
    {

        private bool _hasItems = false;
        private OrderState _state;
        private Location _location;

        private Order()
        {
        }

        public Order(
            Guid orderId,
            Guid orderItemId, 
            Guid menuItemId, 
            IDictionary<string, string> prefernces,
            int quantity)
            : base(orderId)
        {
            var e = new OrderCreated(EventSourceId);
            ApplyEvent(e);

            AddItem(orderItemId, menuItemId, prefernces, quantity);
        }

        public void AddItem(
            Guid orderItemId, 
            Guid menuItemId,
            IDictionary<string, string> preferences,
            int quantity)
        {

            var e = new OrderItemAdded(
                EventSourceId,
                orderItemId,
                menuItemId,
                preferences,
                quantity);

            ApplyEvent(e);

        }

        public void PlaceOrder(Location location)
        {
            switch (_state)
            {
                case OrderState.Created:
                    break;
                case OrderState.Placed:
                    return;
                case OrderState.Cancelled:
                    throw new InvalidAggregateStateException("This order is cancelled. Create a new order.");
                default:
                    throw new InvalidAggregateStateException(string.Empty);
            }

            if (!_hasItems)
                throw new InvalidAggregateStateException("You can't place an empty order. Add an item.");

            var e = new OrderPlaced(EventSourceId, location);
            ApplyEvent(e);


        }

        public void ChangeLocation(Location newLocation)
        {

            switch (_state)
            {
                case OrderState.Created:
                    throw new InvalidAggregateStateException("You can't change the order location before you place the order.");
                case OrderState.Cancelled:
                    throw new InvalidAggregateStateException("You can't change the location of a cancelled order.");
            }

            if (newLocation == _location) 
                return;

            var e = new OrderLocationChanged(EventSourceId, _location, newLocation);
            ApplyEvent(e);
        }

        public void Cancel()
        {

            if (_state == OrderState.Cancelled)
                return;

            var e = new OrderCancelled(EventSourceId);
            ApplyEvent(e);
        }


        protected void On(OrderCreated e)
        {
            _state = OrderState.Created;
        }

        protected void On(OrderItemAdded e)
        {
            _hasItems = true;
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

        protected void On(OrderCancelled e)
        {
            _state = OrderState.Cancelled;
        }

    }

}
