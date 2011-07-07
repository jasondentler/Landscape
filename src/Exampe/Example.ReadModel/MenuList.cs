using Cqrs;
using Example.Menu;

namespace Example.ReadModel
{

    public class MenuList : ViewTable 
    {

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
