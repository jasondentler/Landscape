using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;

namespace Restbucks.ShoppingCart
{

    public class OrderMappings : ICommandMapping
    {

        public void MapCommands(CommandService commandService)
        {

            Map.Command<AddOrderItem>()
                .ToAggregateRoot<Order>()
                .UseExistingOrCreateNew(
                    cmd => cmd.OrderId,
                    cmd => new Order(cmd.OrderId, cmd.OrderItemId, cmd.MenuItemId, cmd.Preferences, cmd.Quantity))
                .ToCallOn((cmd, order) =>
                          order.AddItem(cmd.OrderItemId, cmd.MenuItemId, cmd.Preferences, cmd.Quantity))
                .RegisterWith(commandService);

            Map.Command<PlaceOrder>()
                .ToAggregateRoot<Order>()
                .WithId(cmd => cmd.OrderId)
                .ToCallOn((cmd, order) => order.PlaceOrder(cmd.Location))
                .RegisterWith(commandService);

            Map.Command<ChangeOrderLocation>()
                .ToAggregateRoot<Order>()
                .WithId(cmd => cmd.OrderId)
                .ToCallOn((cmd, order) => order.ChangeLocation(cmd.NewLocation))
                .RegisterWith(commandService);

            Map.Command<CancelOrder>()
                .ToAggregateRoot<Order>()
                .WithId(cmd => cmd.OrderId)
                .ToCallOn((cmd, order) => order.Cancel())
                .RegisterWith(commandService);

        }
    }

}
