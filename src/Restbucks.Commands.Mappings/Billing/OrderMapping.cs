using System.Linq;
using Ncqrs;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;

namespace Restbucks.Billing
{
    public class OrderMapping : ICommandMapping
    {
        public void MapCommands(CommandService commandService)
        {

            Map.Command<CreateOrder>()
                .ToAggregateRoot<Order>()
                .CreateNew(cmd =>
                               {
                                   var productService = NcqrsEnvironment.Get<IProductService>();
                                   var menuItemIds = cmd.Items.Select(i => i.MenuItemId).Distinct();
                                   var productInfos = productService.GetProductInfoByMenuItemIds(menuItemIds);
                                   return new Order(cmd.OrderId, cmd.ShoppingCardOrderId, cmd.Items, productInfos);
                               })
                .RegisterWith(commandService);

            Map.Command<CancelOrder>()
                .ToAggregateRoot<Order>()
                .WithId(cmd => cmd.OrderId)
                .ToCallOn((cmd, order) => order.Cancel())
                .RegisterWith(commandService);



            Map.Command<PayWithCreditCard>()
                .ToAggregateRoot<Order>()
                .WithId(cmd => cmd.OrderId)
                .ToCallOn((cmd, order) => order.PayWithCreditCard(cmd.CardOwner, cmd.CardNumber, cmd.PaymentAmount))
                .RegisterWith(commandService);

        }

    }
}
