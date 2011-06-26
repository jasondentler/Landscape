using System;
using System.Linq;
using Ncqrs.Domain;

namespace Restbucks.Billing
{
    public class Order : AggregateRootMappedByConvention
    {

        private Guid _shoppingCardOrderId;
        private decimal _orderTotal;

        private Order()
        {
        }

        public Order(
            Guid orderId,
            Guid shoppingCardOrderId,
            OrderItemInfo[] items,
            IProductInfo[] products)
            : base(orderId)
        {

            var orderTotal = items
                .Select(i => new
                                 {
                                     i.Quantity,
                                     products.Single(p => p.MenuItemId == i.MenuItemId).Price
                                 })
                .Select(i => i.Quantity*i.Price)
                .Sum();

            var e = new OrderCreated(EventSourceId, shoppingCardOrderId, orderTotal);
            ApplyEvent(e);

        }

        public void PayWithCreditCard(string cardOwner, string cardNumber, decimal paymentAmount)
        {
            var e = new OrderPaid(EventSourceId, _shoppingCardOrderId);
            ApplyEvent(e);
        }

        protected void On(OrderCreated e)
        {
            _shoppingCardOrderId = e.ShoppingCardOrderId;
            _orderTotal = e.OrderTotal;
        }

        protected void On(OrderPaid e)
        {
        }

    }
}
