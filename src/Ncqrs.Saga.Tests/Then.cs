using Ncqrs.Saga.Domain.Shipping;
using SharpTestsEx;
using TechTalk.SpecFlow;

namespace Ncqrs.Saga
{
    [Binding]
    public class Then
    {

        [Then(@"ship the order")]
        public void ThenShipTheOrder()
        {
            var shipmentId = TestHelper.GetId("Shipment");
            var cmd = TestHelper.Then<Ship>();
            cmd.ShipmentId.Should().Be.EqualTo(shipmentId);
        }

        [Then(@"nothing happens")]
        public void ThenNothingHappens()
        {
            TestHelper.DispatchedCommands().Should().Be.Empty();
        }
        
    }
}
