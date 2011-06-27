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
                .CreateNew(cmd => new Order(cmd.OrderId, cmd.Location, cmd.Items))
                .RegisterWith(commandService);

        }
    }
}
