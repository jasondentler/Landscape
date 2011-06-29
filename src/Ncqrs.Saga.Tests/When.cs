using System;
using Ncqrs.Saga.Domain.Billing;
using Ncqrs.Saga.Domain.Shipping;
using Ncqrs.Saga.Sagas;
using TechTalk.SpecFlow;

namespace Ncqrs.Saga
{
    [Binding]
    public class When
    {

        [When(@"the shipment is prepared")]
        public void WhenIHavePreparedAShipment()
        {
            var shipmentId = TestHelper.GetId("Shipment");
            var shippingSagaId = TestHelper.GetId<ShippingSaga>();
            var e = new ShipmentPrepared(shipmentId, shippingSagaId);
            TestHelper.When(e);
        }

        [When(@"the invoice is paid")]
        public void WhenTheInvoiceIsPaid()
        {
            var shippingSagaId = TestHelper.GetId<ShippingSaga>();
            var e = new InvoicePaid(Guid.NewGuid(), shippingSagaId);
            TestHelper.When(e);
        }

    }
}
