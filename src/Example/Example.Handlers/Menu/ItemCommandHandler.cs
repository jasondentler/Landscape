﻿using Cqrs;
using Cqrs.Domain;

namespace Example.Menu
{
    public class ItemCommandHandler : 
        IHandle<AddItem>,
        IHandle<AddCustomization>
    {
        private readonly IRepository _repository;

        public ItemCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(AddItem message)
        {
            var item = new Item(message.MenuItemId, message.Name, message.Price);
            _repository.Save(item);
        }

        public void Handle(AddCustomization message)
        {
            var item = _repository.GetById<Item>(message.MenuItemId);
            item.AddCustomization(message.Name, message.Options);
            _repository.Save(item);
        }
    }
}
