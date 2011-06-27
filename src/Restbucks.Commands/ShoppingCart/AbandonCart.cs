using System;
using Ncqrs.Commanding;

namespace Restbucks.ShoppingCart
{
    public class AbandonCart : CommandBase 
    {
        public Guid CartId { get; private set; }

        public AbandonCart(Guid cartId)
        {
            CartId = cartId;
        }
    }
}
