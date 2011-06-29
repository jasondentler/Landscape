using Ncqrs.Config.Ninject;
using Ninject;
using TechTalk.SpecFlow;

namespace Ncqrs.Saga
{
    [Binding]
    public class Setup
    {

        [BeforeScenario("saga")]
        public void OnSetup()
        {
            if (NcqrsEnvironment.IsConfigured)
                return;

            var kernel = new StandardKernel(new SagaModule());
            NcqrsEnvironment.Configure(new NinjectConfiguration(kernel));
        }

    }
}
