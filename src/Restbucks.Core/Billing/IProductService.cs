using System;
using System.Collections.Generic;

namespace Restbucks.Billing
{

    public interface IProductService
    {
        IProductInfo GetProductInfoByMenuItemId(Guid menuItemId);
        IProductInfo[] GetProductInfoByMenuItemIds(IEnumerable<Guid> menuItemIds);
    }

    public interface IProductInfo
    {
        Guid ProductId { get; }
        Guid MenuItemId { get; }
        string Name { get; }
        decimal Price { get; }
    }


}
