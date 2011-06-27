using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Restbucks.Billing;

namespace Restbucks.ShoppingCart
{

    public class CartMappings : ICommandMapping
    {

        public void MapCommands(CommandService commandService)
        {

            Map.Command<AddItem>()
                .ToAggregateRoot<Cart>()
                .UseExistingOrCreateNew(
                    cmd => cmd.CartId,
                    cmd => new Cart(cmd.CartId, cmd.ItemId, cmd.MenuItemId, cmd.Preferences, cmd.Quantity))
                .ToCallOn((cmd, cart) =>
                          cart.AddItem(cmd.ItemId, cmd.MenuItemId, cmd.Preferences, cmd.Quantity))
                .RegisterWith(commandService);

            Map.Command<PlaceOrder>()
                .ToAggregateRoot<Cart>()
                .WithId(cmd => cmd.CartId)
                .ToCallOn((cmd, cart) => cart.PlaceOrder(cmd.Location))
                .RegisterWith(commandService);

            Map.Command<ChangeLocation>()
                .ToAggregateRoot<Cart>()
                .WithId(cmd => cmd.CartId)
                .ToCallOn((cmd, cart) => cart.ChangeLocation(cmd.NewLocation))
                .RegisterWith(commandService);

            Map.Command<AbandonCart>()
                .ToAggregateRoot<Cart>()
                .WithId(cmd => cmd.CartId)
                .ToCallOn((cmd, cart) => cart.Abandon())
                .RegisterWith(commandService);

        }
    }

}
