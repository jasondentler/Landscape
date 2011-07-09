using System;
using System.Collections.Generic;
using Cqrs;
using Example.Menu;

namespace Example.ReadModel
{

    public class MenuList : ViewTable 
    {

        public Model GetAllItems(int pageNumber, int itemsPerPage)
        {
            var items = GetItems(pageNumber, itemsPerPage);
            var itemCount = CountItems();
            var pageCount = (int) Math.Floor(Convert.ToDecimal(itemCount)/Convert.ToDecimal(itemsPerPage))
                            + (itemCount%itemsPerPage == 0 ? 0 : 1);
            return new Model()
                       {
                           Items = items,
                           ItemsPerPage = itemsPerPage,
                           PageCount = pageCount,
                           PageNumber = pageNumber
                       };
        }

        private IEnumerable<Item> GetItems(int pageNumber, int itemsPerPage)
        {
            var skip = (pageNumber - 1) * itemsPerPage;
            return Db.MenuList.All().OrderByName().Skip(skip).Take(itemsPerPage).ToArray<Item>();
        }

        private long CountItems()
        {
            return Db.MenuList.Count();
        }

        public class Model
        {
            public int ItemsPerPage { get; set; }
            public int PageNumber { get; set; }
            public int PageCount { get; set; }
            public IEnumerable<Item> Items { get; set; }
        }

        public class Item 
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }


        private class Denormalizer : ViewTable,
            IHandle<ItemAdded>
        {
            public void Handle(ItemAdded message)
            {
                Db.MenuList.Insert(Id: message.MenuItemId, Name: message.Name, Price: message.Price);
            }
        }

    }

}
