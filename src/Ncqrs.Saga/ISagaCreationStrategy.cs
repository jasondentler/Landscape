namespace Ncqrs.Saga
{
    
    public interface ISagaCreationStrategy
    {

        TSaga CreateSaga<TSaga>();

    }


}
