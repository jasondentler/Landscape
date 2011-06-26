using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs.Domain;
using Restbucks.Billing;

namespace Restbucks.ShoppingCart
{
    public class Order : AggregateRootMappedByConvention
    {

        private enum OrderState
        {

            Created,
            Placed,
            Abandoned,

        }



        private readonly HashSet<OrderItem> _items = new HashSet<OrderItem>();
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
                case OrderState.Abandoned:
                    throw new InvalidAggregateStateException("This order is cancelled. Create a new order.");
                default:
                    throw new InvalidAggregateStateException(string.Empty);
            }

            if (!_items.Any())
                throw new InvalidAggregateStateException("You can't place an empty order. Add an item.");

            var e = new OrderPlaced(EventSourceId, location, GetOrderItemInfo());
            ApplyEvent(e);


        }

        public void ChangeLocation(Location newLocation)
        {

            switch (_state)
            {
                case OrderState.Created:
                    throw new InvalidAggregateStateException("You can't change the order location before you place the order.");
                case OrderState.Abandoned:
                    throw new InvalidAggregateStateException("You can't change the location of a cancelled order.");
            }

            if (newLocation == _location) 
                return;

            var e = new OrderLocationChanged(EventSourceId, _location, newLocation);
            ApplyEvent(e);
        }

        public void Cancel()
        {

            if (_state == OrderState.Abandoned)
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
            _items.Add(new OrderItem(this, e));
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
            _state = OrderState.Abandoned;
        }

        private OrderItemInfo[] GetOrderItemInfo()
        {
            return _items.Select(item => GetOrderItemInfo(item)).ToArray();
        }

        private OrderItemInfo GetOrderItemInfo(OrderItem orderItem)
        {
            return new OrderItemInfo(
                orderItem.OrderItemId,
                orderItem.MenuItemId,
                orderItem.Preferences,
                orderItem.Quantity);
        }


    }

}
