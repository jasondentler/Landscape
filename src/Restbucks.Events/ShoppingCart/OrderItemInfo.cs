using System;
using System.Collections.Generic;
using System.Linq;

namespace Restbucks.ShoppingCart
{
    public class OrderItemInfo : IComponent
    {

        public Guid OrderItemId { get; private set; }
        public Guid MenuItemId { get; private set; }
        public IDictionary<string, string> Preferences { get; private set; }
        public int Quantity { get; private set; }

        public OrderItemInfo(
            Guid orderItemId,
            Guid menuItemId,
            IDictionary<string, string> preferences,
            int quantity)
        {
            if (preferences == null)
                throw new NullReferenceException("preferences");

            OrderItemId = orderItemId;
            MenuItemId = menuItemId;
            Preferences = preferences.ToDictionary(i => i.Key, i => i.Value);
            Quantity = quantity;
        }

        public override string ToString()
        {
            return string.Format("Order Item {0}: {1}x menu item {2}, {3}",
                                 OrderItemId,
                                 Quantity,
                                 MenuItemId.ToString(),
                                 string.Join(" ,", Preferences.Select(i => i.Value + " " + i.Key)) ?? "");
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as OrderItemInfo);
        }

        public bool Equals(OrderItemInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.OrderItemId.Equals(OrderItemId) && other.MenuItemId.Equals(MenuItemId) && DictionariesAreEqual(other.Preferences, Preferences) && other.Quantity == Quantity;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = OrderItemId.GetHashCode();
                result = (result*397) ^ MenuItemId.GetHashCode();
                result = (result*397) ^ (Preferences != null ? Preferences.GetHashCode() : 0);
                result = (result*397) ^ Quantity;
                return result;
            }
        }

        private static bool DictionariesAreEqual(IDictionary<string, string> a, IDictionary<string, string> b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (ReferenceEquals(null, a) || ReferenceEquals(null, b))
                return false;
            if (a.Count != b.Count)
                return false;
            var aValues = a.OrderBy(i => i.Key).Select(i => i.Value);
            var bValues = a.OrderBy(i => i.Key).Select(i => i.Value);
            return aValues.Zip(bValues, (aValue, bValue) => new[] {aValue, bValue})
                .All(i => i[0] == i[1]);
        }

    }
}
