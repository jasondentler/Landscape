using Ncqrs;
using Ncqrs.Config.Ninject;
using Ninject;
using TechTalk.SpecFlow;

namespace Restbucks
{
    [Binding]
    public class Setup
    {

        [BeforeScenario("domain")]
        public void OnSetup()
        {
            if (NcqrsEnvironment.IsConfigured)
                return;

            var kernel = new StandardKernel(new NcqrsModule());
            NcqrsEnvironment.Configure(new NinjectConfiguration(kernel));
        }

    }
}
