using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;

namespace Restbucks.Billing
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
