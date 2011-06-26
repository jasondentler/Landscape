using System;
using Ncqrs.Commanding;

namespace Restbucks.Billing
{
    public class PayWithCreditCard : CommandBase 
    {
        public Guid OrderId { get; private set; }
        public string CardOwner { get; private set; }
        public string CardNumber { get; private set; }
        public decimal PaymentAmount { get; private set; }

        public PayWithCreditCard(
            Guid orderId,
            string cardOwner,
            string cardNumber,
            decimal paymentAmount)
        {
            OrderId = orderId;
            CardOwner = cardOwner;
            CardNumber = cardNumber;
            PaymentAmount = paymentAmount;
        }
    }
}
