using System;

namespace Restbucks.ShoppingCart
{
    public class CartCreated  : IEvent 
    {
        public Guid CartId { get; private set; }

        public CartCreated(Guid cartId)
        {
            CartId = cartId;
        }
    }
}
