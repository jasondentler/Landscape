using System;
using System.Linq;
using Ncqrs.Domain;

namespace Restbucks.Billing
{
    public class Order : AggregateRootMappedByConvention
    {

        private enum State
        {
            Placed,
            Paid,
            Cancelled
        }
        
        private Guid _shoppingCartOrderId;
        private Guid _deliverySagaId;
        private decimal _orderTotal;

        private bool _isPaid;
        private bool _isCancelled;

        private Order()
        {
        }

        public Order(
            Guid orderId,
            Guid shoppingCartOrderId,
            OrderItemInfo[] items,
            IProductInfo[] products,
            Guid deliverySagaId)
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

            var e = new OrderPlaced(EventSourceId, shoppingCartOrderId, orderTotal, deliverySagaId);
            ApplyEvent(e);

        }

        public void Cancel()
        {
            if (_isCancelled)
                return;

            if (_isPaid)
                throw new InvalidAggregateStateException("You can't cancel this order. You've already paid for it.");

            var e = new OrderCancelled(EventSourceId);
            ApplyEvent(e);
        }

        public void PayWithCreditCard(string cardOwner, string cardNumber, decimal paymentAmount)
        {

            if (_isPaid)
                throw new InvalidAggregateStateException("This order is already paid for. Have a nice day.");

            if (paymentAmount != _orderTotal)
                throw new InvalidAggregateStateException("Incorrect amount. Your order total is {0:C}.", _orderTotal);

            var e = new OrderPaid(EventSourceId, _shoppingCartOrderId, _deliverySagaId);
            ApplyEvent(e);
        }

        protected void On(OrderPlaced e)
        {
            _shoppingCartOrderId = e.ShoppingCartOrderId;
            _deliverySagaId = e.DeliverySagaId;
            _orderTotal = e.OrderTotal;
        }

        protected void On(OrderCancelled e)
        {
            _isCancelled = true;
        }

        protected void On(OrderPaid e)
        {
            _isPaid = true;
        }

    }
}
