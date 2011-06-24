using System.Collections.Generic;
using Landscape.Core;

namespace Restbucks.UI.Models.Menu.ViewModels
{
    public class Index : JsonSerializable 
    {

        public IDictionary<string, decimal> Menu { get; set; }

        public Index(IDictionary<string, decimal> menu)
        {
            Menu = menu;
        }
    }
}