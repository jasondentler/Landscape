using System.Collections.Generic;
using Cqrs;
using Example.Menu;

namespace Example.Denormalizers
{

    public class MenuList :
        IHandle<ItemAdded>
    {
        private readonly ReadModel _readModel;

        public MenuList(ReadModel readModel)
        {
            _readModel = readModel;
        }

        public void Handle(ItemAdded message)
        {
            _readModel.Insert("MenuList", new Dictionary<string, object>()
                                              {
                                                  {"MenuItemId", message.MenuItemId},
                                                  {"Name", message.Name},
                                                  {"Price", message.Price}
                                              });
        }
    }

}
