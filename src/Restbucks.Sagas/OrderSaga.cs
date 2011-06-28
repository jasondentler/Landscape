using Ncqrs.Saga;
using Restbucks.ShoppingCart;

namespace Restbucks.Sagas
{
    public class OrderSaga : SagaBase  
    {

        private OrderSaga()
        {
        }

        public OrderSaga(OrderPlaced e)
        {
            ApplyEvent(e);
        }

        public void LocationChanged(LocationChanged e)
        {
            ApplyEvent(e);
        }

        protected void On(OrderPlaced e)
        {
        }

        protected void On(LocationChanged e)
        {
        }

    }
}
