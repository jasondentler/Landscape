using System;
using System.Collections.Generic;
using System.Linq;

namespace Restbucks.Barista
{
    public class OrderItemInfo
    {

        public Guid MenuItemId { get; private set; }
        public IDictionary<string, string> Preferences { get; private set; }
        public int Quantity { get; private set; }

        public OrderItemInfo(
            Guid menuItemId,
            IDictionary<string, string> preferences,
            int quantity)
        {
            if (preferences == null)
                throw new NullReferenceException("preferences");

            MenuItemId = menuItemId;
            Preferences = preferences.ToDictionary(i => i.Key, i => i.Value);
            Quantity = quantity;
        }

        public override string ToString()
        {
            return string.Format("{0}x menu item {1}, {2}",
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
            return other.MenuItemId.Equals(MenuItemId) && DictionariesAreEqual(other.Preferences, Preferences) && other.Quantity == Quantity;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = MenuItemId.GetHashCode();
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
