using System.Linq;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Restbucks
{

    /// <remarks>>
    /// This saves a LOT of headaches trying to remember all the different AR Ids 
    /// </remarks>
    public class AggregateRootCreatedHandler :
        IEventHandler<Menu.MenuItemAdded>, 
        IEventHandler<ShoppingCart.CartCreated>,
        IEventHandler<Billing.ProductAdded>,
        IEventHandler<Billing.OrderPlaced>
    {

        public void RegisterWith(InProcessEventBus eventBus)
        {
            eventBus.RegisterHandler<Menu.MenuItemAdded>(this);
            eventBus.RegisterHandler<ShoppingCart.CartCreated>(this);
            eventBus.RegisterHandler<Billing.ProductAdded>(this);
            eventBus.RegisterHandler<Billing.OrderPlaced>(this);
        }

        public void Handle(IPublishedEvent<Menu.MenuItemAdded> evnt)
        {
            var e = evnt.Payload;
            AggregateRootHelper.SetIdFor<Menu.MenuItem>(e.MenuItemId, e.Name);
        }

        public void Handle(IPublishedEvent<ShoppingCart.CartCreated> evnt)
        {
            var e = evnt.Payload;
            AggregateRootHelper.SetIdFor<ShoppingCart.Cart>(e.CartId);
        }

        public void Handle(IPublishedEvent<Billing.ProductAdded> evnt)
        {
            var e = evnt.Payload;
            AggregateRootHelper.SetIdFor<Billing.Product>(e.ProductId, e.Name);
        }

        public void Handle(IPublishedEvent<Billing.OrderPlaced> evnt)
        {
            var e = evnt.Payload;
            AggregateRootHelper.SetIdFor<Billing.Order>(e.OrderId);
        }
    }


}
