using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs.Domain;
using Restbucks.Billing;

namespace Restbucks.ShoppingCart
{
    public class Cart : AggregateRootMappedByConvention
    {

        private enum CartState
        {

            Created,
            Placed,
            Abandoned,

        }



        private readonly HashSet<CartItem> _items = new HashSet<CartItem>();
        private CartState _state;
        private Location _location;

        private Cart()
        {
        }

        public Cart(
            Guid cartId,
            Guid cartItemId, 
            Guid menuItemId, 
            IDictionary<string, string> prefernces,
            int quantity)
            : base(cartId)
        {
            var e = new CartCreated(EventSourceId);
            ApplyEvent(e);

            AddItem(cartItemId, menuItemId, prefernces, quantity);
        }

        public void AddItem(
            Guid cartItemId, 
            Guid menuItemId,
            IDictionary<string, string> preferences,
            int quantity)
        {

            var e = new ItemAdded(
                EventSourceId,
                cartItemId,
                menuItemId,
                preferences,
                quantity);

            ApplyEvent(e);

        }

        public void PlaceOrder(Location location)
        {
            switch (_state)
            {
                case CartState.Created:
                    break;
                case CartState.Placed:
                    return;
                case CartState.Abandoned:
                    throw new InvalidAggregateStateException("This shopping cart is abandoned. Create a new order.");
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
                case CartState.Created:
                    throw new InvalidAggregateStateException("You can't change the location before you place the order.");
                case CartState.Abandoned:
                    throw new InvalidAggregateStateException(
                        "You can't change the location. This shopping cart is abandoned.");
            }

            if (newLocation == _location) 
                return;

            var e = new LocationChanged(EventSourceId, _location, newLocation);
            ApplyEvent(e);
        }

        public void Cancel()
        {

            if (_state == CartState.Abandoned)
                return;

            var e = new OrderCancelled(EventSourceId);
            ApplyEvent(e);
        }


        protected void On(CartCreated e)
        {
            _state = CartState.Created;
        }

        protected void On(ItemAdded e)
        {
            _items.Add(new CartItem(this, e));
        }

        protected void On(OrderPlaced e)
        {
            _state = CartState.Placed;
            _location = e.Location;
        }

        protected void On(LocationChanged e)
        {
            _location = e.Location;
        }

        protected void On(OrderCancelled e)
        {
            _state = CartState.Abandoned;
        }

        private OrderItemInfo[] GetOrderItemInfo()
        {
            return _items.Select(item => GetOrderItemInfo(item)).ToArray();
        }

        private OrderItemInfo GetOrderItemInfo(CartItem cartItem)
        {
            return new OrderItemInfo(
                cartItem.ItemId,
                cartItem.MenuItemId,
                cartItem.Preferences,
                cartItem.Quantity);
        }


    }

}
