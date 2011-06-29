using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;

namespace Ncqrs.Saga
{
    public class NullCommandService : ICommandService 
    {
        public void Execute(ICommand command)
        {
            
        }
    }
}
