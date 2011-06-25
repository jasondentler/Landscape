using System;
using Ncqrs.Commanding;

namespace Restbucks.Menu
{

    public class AddCustomization : CommandBase 
    {

        public Guid ProductId { get; private set; }
        public string Customization { get; private set; }
        public string[] Options { get; private set; }

        public AddCustomization(
            Guid productId,
            string customization, 
            params string[] options)
        {
            ProductId = productId;
            Customization = customization;
            Options = options;
        }
    }

}
