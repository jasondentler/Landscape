using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;

namespace Restbucks.Payment
{
    public class ProductMapping : ICommandMapping
    {
        public void MapCommands(CommandService commandService)
        {

            Map.Command<AddProduct>()
                .ToAggregateRoot<Product>()
                .CreateNew(cmd => new Product(cmd.ProductId, cmd.MenuProductId, cmd.Name, cmd.Price))
                .RegisterWith(commandService);

        }
    }
}
