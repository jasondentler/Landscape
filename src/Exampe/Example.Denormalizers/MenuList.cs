using Cqrs;
using Example.Menu;

namespace Example.Denormalizers
{

    public class MenuList : ReadModel 
    {

        private class Denormalizer : ReadModel,
            IHandle<ItemAdded>
        {
            public void Handle(ItemAdded message)
            {
                Db.MenuList.Insert(Id: message.MenuItemId, Name: message.Name, Price: message.Price);
            }
        }

    }

}
