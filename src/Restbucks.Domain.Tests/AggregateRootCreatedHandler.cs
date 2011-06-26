using System.Linq;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Restbucks
{

    /// <remarks>>
    /// This saves a LOT of headaches trying to remember all the different AR Ids 
    /// </remarks>
    public class AggregateRootCreatedHandler :
        IEventHandler<Menu.MenuItemAdded>, 
        IEventHandler<ShoppingCart.OrderCreated>,
        IEventHandler<Billing.ProductAdded>,
        IEventHandler<Billing.OrderCreated>
    {

        public void RegisterWith(InProcessEventBus eventBus)
        {
            eventBus.RegisterHandler<Menu.MenuItemAdded>(this);
            eventBus.RegisterHandler<ShoppingCart.OrderCreated>(this);
            eventBus.RegisterHandler<Billing.ProductAdded>(this);
            eventBus.RegisterHandler<Billing.OrderCreated>(this);
        }

        public void Handle(IPublishedEvent<Menu.MenuItemAdded> evnt)
        {
            var e = evnt.Payload;
            AggregateRootHelper.SetIdFor<Menu.MenuItem>(e.MenuItemId, e.Name);
        }

        public void Handle(IPublishedEvent<ShoppingCart.OrderCreated> evnt)
        {
            var e = evnt.Payload;
            AggregateRootHelper.SetIdFor<ShoppingCart.Order>(e.OrderId);
        }

        public void Handle(IPublishedEvent<Billing.ProductAdded> evnt)
        {
            var e = evnt.Payload;
            AggregateRootHelper.SetIdFor<Billing.Product>(e.ProductId, e.Name);
        }

        public void Handle(IPublishedEvent<Billing.OrderCreated> evnt)
        {
            var e = evnt.Payload;
            AggregateRootHelper.SetIdFor<Billing.Order>(e.OrderId);
        }
    }


}
