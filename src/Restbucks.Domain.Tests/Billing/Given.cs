using TechTalk.SpecFlow;

namespace Restbucks.Billing
{
    [Binding]
    public class Given
    {

        //public void TheOrderIsCreated()
        //{
        //    var shoppingCardOrderId = DomainHelper.GetId<ShoppingCart.Order>();
        //    var orderId = Guid.NewGuid();

        //    var shoppingCardItems = DomainHelper.GetAllEvents(shoppingCardOrderId)
        //        .OfType<ShoppingCart.OrderItemAdded>();

        //    var products = DomainHelper.GetGivenEvents()
        //        .OfType<Menu.MenuItemAdded>();

        //    var orderTotal = shoppingCardItems
        //        .Select(i => new
        //                         {
        //                             i.Quantity,
        //                             products.Single(p => p.MenuItemId == i.MenuItemId).Price
        //                         })
        //        .Select(i => i.Quantity*i.Price)
        //        .Sum();

        //    var e = new OrderCreated(orderId, shoppingCardOrderId, orderTotal);

        //    DomainHelper.SetId<Order>(orderId);
        //    DomainHelper.GivenEvent<Order>(e);

        //}

    }
}
