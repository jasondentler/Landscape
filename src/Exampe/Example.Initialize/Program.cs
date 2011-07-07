using System;
using System.Linq;
using Ninject;
using Ninject.Modules;

namespace Example.Initialize
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            var kernel = Wire();
            kernel.Get<EventStoreInitializer>().Initialize();
            kernel.Get<MenuInitializer>().Initialize();
        }

        private static IKernel Wire()
        {
            var asm = typeof (Example.Wiring.CqrsModule).Assembly;
            var modules = asm.GetTypes()
                .Where(t => typeof (NinjectModule).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<NinjectModule>()
                .ToArray();
            return new StandardKernel(modules);
        }

    }
}
