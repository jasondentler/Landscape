using System;

namespace Ncqrs.Saga.Domain.Billing
{
    public class InvoicePaid
    {
        public Guid InvoiceId { get; private set; }
        public Guid ShippingSagaId { get; private set; }

        public InvoicePaid(Guid invoiceId, Guid shippingSagaId)
        {
            InvoiceId = invoiceId;
            ShippingSagaId = shippingSagaId;
        }
    }
}