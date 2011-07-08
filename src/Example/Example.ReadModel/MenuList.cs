using System;
using System.Collections.Generic;
using Cqrs;
using Example.Menu;

namespace Example.ReadModel
{

    public class MenuList : ViewTable 
    {

        public IEnumerable<Item> GetAllItems(int pageNumber, int itemsPerPage)
        {
            var skip = (pageNumber - 1)*itemsPerPage;
            return Db.MenuList.All().OrderByName().Skip(skip).Take(itemsPerPage).Cast<Item>();
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
