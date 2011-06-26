using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Restbucks.Billing
{

    /// <summary>
    /// Provides product catalog for Billing BC
    /// </summary>
    /// <remarks>
    /// This would normally be stored in a database, not an in-memory dictionary.
    /// </remarks>
    public class ProductService
        : IProductService, IEventHandler<ProductAdded>
    {

        private static readonly ConcurrentDictionary<Guid, IProductInfo> ProductsByMenuItemId =
            new ConcurrentDictionary<Guid, IProductInfo>();

        public IProductInfo GetProductInfoByMenuItemId(Guid menuItemId)
        {
            IProductInfo productInfo;
            if (!ProductsByMenuItemId.TryGetValue(menuItemId, out productInfo))
                throw new ApplicationException(string.Format("The product service doesn't have menu item {0}",
                                                             menuItemId));
            return productInfo;
        }

        public IProductInfo[] GetProductInfoByMenuItemIds(IEnumerable<Guid> menuItemIds)
        {
            return menuItemIds.Select(id => GetProductInfoByMenuItemId(id)).ToArray();
        }

        public void Handle(IPublishedEvent<ProductAdded> evnt)
        {
            var e = evnt.Payload;
            ProductsByMenuItemId.AddOrUpdate(
                e.MenuItemId,
                id => new ProductInfo(e.ProductId, e.MenuItemId, e.Name, e.Price),
                (id, info) => new ProductInfo(e.ProductId, e.MenuItemId, e.Name, e.Price));
        }

        public class ProductInfo
            : IProductInfo
        {
            public Guid ProductId { get; private set; }
            public Guid MenuItemId { get; private set; }
            public string Name { get; private set; }
            public decimal Price { get; private set; }

            public ProductInfo(
                Guid productId,
                Guid menuItemId,
                string name,
                decimal price)
            {
                ProductId = productId;
                MenuItemId = menuItemId;
                Name = name;
                Price = price;
            }
        }
    }
}
