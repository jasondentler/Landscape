using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing.ServiceModel.Bus;
using Restbucks.Menu;

namespace Restbucks.Billing
{
    public class MenuItemAddedHandler
        : IEventHandler<MenuItemAdded>
    {
        private readonly IUniqueIdentifierGenerator _idGenerator;
        private readonly ICommandService _commandService;

        public MenuItemAddedHandler(
            IUniqueIdentifierGenerator idGenerator,
            ICommandService commandService)
        {
            _idGenerator = idGenerator;
            _commandService = commandService;
        }

        public void Handle(IPublishedEvent<MenuItemAdded> evnt)
        {

            var e = evnt.Payload;
            var productId = _idGenerator.GenerateNewId();

            var cmd = new AddProduct(
                productId,
                e.MenuItemId,
                e.Name,
                e.Price);

            _commandService.Execute(cmd);
        }
    }
}
