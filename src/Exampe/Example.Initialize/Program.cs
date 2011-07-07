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
            new Program().Run();
        }

        private void Run()
        {
            var kernel = Wire();
            kernel.Get<MenuInitializer>().Initialize();
        }

        private IKernel Wire()
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
