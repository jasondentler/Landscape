using System;
using Ncqrs.Saga.Domain.Billing;
using Ncqrs.Saga.Domain.Shipping;
using Ncqrs.Saga.Sagas;
using TechTalk.SpecFlow;

namespace Ncqrs.Saga
{
    [Binding]
    public class Given
    {

        [Given(@"the shipment is prepared")]
        public void GivenTheShipmentIsPrepared()
        {
            var shipmentId = TestHelper.GetId("Shipment");
            var shippingSagaId = TestHelper.GetId<ShippingSaga>();
            var e = new ShipmentPrepared(shipmentId, shippingSagaId);
            TestHelper.Given(e);
        }

        [Given(@"the invoice is paid")]
        public void GivenTheInvoiceIsPaid()
        {
            var shippingSagaId = TestHelper.GetId<ShippingSaga>();
            var e = new InvoicePaid(Guid.NewGuid(), shippingSagaId);
            TestHelper.Given(e);
        }

    }
}
