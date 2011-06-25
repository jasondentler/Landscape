using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Config.Ninject;
using Ncqrs.Domain.Storage;
using Ninject;
using Ninject.Modules;

namespace Restbucks.Domain.Tests
{
    public class NcqrsModule : NinjectModule
    {

        public override void Load()
        {

            Kernel.Bind<IKernel>().ToConstant(Kernel);

            var commandService = new CommandService();
            commandService.Configure();
            Kernel.Bind<ICommandService>().ToConstant(commandService);

        }
    }
}
