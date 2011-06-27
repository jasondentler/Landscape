using System.Collections.Generic;
using System.Linq;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Restbucks.Barista
{

    public class OrderPlacedHandler :
        IEventHandler<ShoppingCart.OrderPlaced>
    {
        private readonly IUniqueIdentifierGenerator _idGenerator;
        private readonly ICommandService _commandService;

        public OrderPlacedHandler(
            IUniqueIdentifierGenerator idGenerator,
            ICommandService commandService)
        {
            _idGenerator = idGenerator;
            _commandService = commandService;
        }

        public void Handle(IPublishedEvent<ShoppingCart.OrderPlaced> evnt)
        {
            var orderId = _idGenerator.GenerateNewId();
            var e = evnt.Payload;

            var cmd = new QueueOrder(
                orderId,
                e.Location,
                Convert(e.Items));

            _commandService.Execute(cmd);

        }

        private OrderItemInfo[] Convert(IEnumerable<Billing.OrderItemInfo> items)
        {
            return items
                .Select(Convert)
                .ToArray();
        }

        private OrderItemInfo Convert(Billing.OrderItemInfo item)
        {
            return new OrderItemInfo(
                item.MenuItemId,
                item.Preferences
                    .ToDictionary(i => i.Key, i => i.Value),
                item.Quantity);
        }

    }

}
