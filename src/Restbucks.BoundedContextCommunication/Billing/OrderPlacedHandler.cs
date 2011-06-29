using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing.ServiceModel.Bus;
using Restbucks.ShoppingCart;

namespace Restbucks.Billing
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

            var cmd = new CreateOrder(
                orderId,
                e.CartId,
                e.Items,
                e.DeliverySagaId);

            _commandService.Execute(cmd);

        }
    }

}
