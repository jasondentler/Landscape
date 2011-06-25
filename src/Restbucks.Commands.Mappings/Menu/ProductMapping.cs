using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;

namespace Restbucks.Menu
{

    public class ProductMapping : ICommandMapping
    {

        public void MapCommands(CommandService commandService)
        {

            Map.Command<AddProduct>()
                .ToAggregateRoot<Product>()
                .CreateNew(cmd => new Product(cmd.ProductId, cmd.Name, cmd.Price))
                .RegisterWith(commandService);

            Map.Command<AddCustomization>()
                .ToAggregateRoot<Product>()
                .WithId(cmd => cmd.ProductId)
                .ToCallOn((cmd, product) => product.AddCustomization(cmd.Customization, cmd.Options))
                .RegisterWith(commandService);


        }
    }

}
