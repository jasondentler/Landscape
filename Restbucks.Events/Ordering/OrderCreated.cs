﻿using System;
using Ncqrs.Eventing.Sourcing;

namespace Restbucks.Ordering
{
    public class OrderCreated : SourcedEvent 
    {
        public Guid OrderId { get; private set; }

        public OrderCreated(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
