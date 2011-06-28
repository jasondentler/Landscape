using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Saga.Mapping;
using Restbucks.ShoppingCart;

namespace Restbucks.Sagas
{

    public class OrderSagaMapping
    {

        public void RegisterMappings(InProcessEventBus eventBus)
        {

            Map.Event<OrderPlaced>()
                .ToSaga<OrderSaga>()
                .CreateNew(e => new OrderSaga(e))
                .RegisterWith(eventBus);

            Map.Event<LocationChanged>()
                .ToSaga<OrderSaga>()
                .WithId(e => e.CartId)
                .ToCallOn((e, order) => order.LocationChanged(e))
                .RegisterWith(eventBus);

        }

    }

}
