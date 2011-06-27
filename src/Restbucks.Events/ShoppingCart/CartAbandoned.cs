using System;

namespace Restbucks.ShoppingCart
{
    public class CartAbandoned : IEvent 
    {
        public Guid CartId { get; private set; }

        public CartAbandoned(Guid cartId)
        {
            CartId = cartId;
        }
    }
}
