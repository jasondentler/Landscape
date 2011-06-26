using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Restbucks.Payment
{
    public class ProductHandler
        : IEventHandler<Menu.ProductAdded>
    {
        private readonly IUniqueIdentifierGenerator _idGenerator;
        private readonly ICommandService _commandService;

        public ProductHandler(
            IUniqueIdentifierGenerator idGenerator,
            ICommandService commandService)
        {
            _idGenerator = idGenerator;
            _commandService = commandService;
        }

        public void Handle(IPublishedEvent<Menu.ProductAdded> evnt)
        {

            var e = evnt.Payload;
            var productId = _idGenerator.GenerateNewId();

            var cmd = new Restbucks.Payment.AddProduct(
                productId,
                e.ProductId,
                e.Name,
                e.Price);

            _commandService.Execute(cmd);
        }
    }
}
