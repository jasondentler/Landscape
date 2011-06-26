﻿using System;
using Ncqrs.Commanding;

namespace Restbucks.Menu
{

    public class AddProduct : CommandBase 
    {
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public AddProduct(
            Guid productId,
            string name,
            decimal price)
        {
            ProductId = productId;
            Name = name;
            Price = price;
        }
    }

}
