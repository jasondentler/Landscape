using System;

namespace Restbucks.Menu
{
    public class CustomizationAdded
    {

        public Guid ProductId { get; private set; }
        public string Customization { get; private set; }
        public string[] Options { get; private set; }

        public CustomizationAdded(
            Guid productId,
            string customization,
            string[] options)
        {
            ProductId = productId;
            Customization = customization;
            Options = options;
        }
    }
}
