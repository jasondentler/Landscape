using Ncqrs.Eventing.ServiceModel.Bus;
using Restbucks.Sagas;
using Restbucks.ShoppingCart;

namespace Restbucks
{

    /// <remarks>>
    /// This saves a LOT of headaches trying to remember all the different AR Ids 
    /// </remarks>
    public class EntityCreatedHandler :
        IEventHandler<Menu.MenuItemAdded>,
        IEventHandler<ShoppingCart.CartCreated>,
        IEventHandler<ShoppingCart.ItemAdded>,
        IEventHandler<ShoppingCart.OrderPlaced>,
        IEventHandler<Billing.ProductAdded>,
        IEventHandler<Billing.OrderPlaced>,
        IEventHandler<Barista.OrderQueued>
    {

        public void RegisterWith(InProcessEventBus eventBus)
        {
            eventBus.RegisterHandler<Menu.MenuItemAdded>(this);
            eventBus.RegisterHandler<ShoppingCart.CartCreated>(this);
            eventBus.RegisterHandler<ShoppingCart.ItemAdded>(this);
            eventBus.RegisterHandler<ShoppingCart.OrderPlaced>(this);
            eventBus.RegisterHandler<Billing.ProductAdded>(this);
            eventBus.RegisterHandler<Billing.OrderPlaced>(this);
            eventBus.RegisterHandler<Barista.OrderQueued>(this);
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

        public void Handle(IPublishedEvent<ShoppingCart.ItemAdded> evnt)
        {
            var e = evnt.Payload;
            AggregateRootHelper.SetIdFor<ShoppingCart.CartItem>(e.ItemId);
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

        public void Handle(IPublishedEvent<Barista.OrderQueued> evnt)
        {
            var e = evnt.Payload;
            AggregateRootHelper.SetIdFor<Barista.Order>(e.OrderId);
        }

        public void Handle(IPublishedEvent<OrderPlaced> evnt)
        {
            var e = evnt.Payload;
            AggregateRootHelper.SetIdFor<DeliverySaga>(e.DeliverySagaId);
        }

    }

}
