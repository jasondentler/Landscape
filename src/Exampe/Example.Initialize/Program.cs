using System;
using Example.Wiring;
using Ninject;

namespace Example.Initialize
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            var kernel = KernelFactory.ConfigureKernel();
            kernel.Get<EventStoreInitializer>().Initialize();
            kernel.Get<MenuInitializer>().Initialize();
            Console.WriteLine("Landscape example initialized. Press any key.");
            Console.ReadKey();
        }


    }
}
