using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;

namespace Restbucks.Barista
{
    public class OrderMappings : ICommandMapping
    {
        public void MapCommands(CommandService commandService)
        {

            Map.Command<QueueOrder>()
                .ToAggregateRoot<Order>()
                .CreateNew(cmd => new Order(cmd.OrderId, cmd.Location, cmd.Items, cmd.DeliverySagaId))
                .RegisterWith(commandService);

            Map.Command<BeginPreparingOrder>()
                .ToAggregateRoot<Order>()
                .WithId(cmd => cmd.OrderId)
                .ToCallOn((cmd, order) => order.BeginPreparingOrder())
                .RegisterWith(commandService);

            Map.Command<FinishPreparingOrder>()
                .ToAggregateRoot<Order>()
                .WithId(cmd => cmd.OrderId)
                .ToCallOn((cmd, order) => order.FinishPreparingOrder())
                .RegisterWith(commandService);

            Map.Command<DeliverOrder>()
                .ToAggregateRoot<Order>()
                .WithId(cmd => cmd.OrderId)
                .ToCallOn((cmd, order) => order.Deliver())
                .RegisterWith(commandService);

        }
    }
}
