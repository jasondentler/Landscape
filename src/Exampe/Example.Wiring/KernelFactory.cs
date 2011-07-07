﻿using System;
using System.Linq;
using System.Reflection;
using Ninject;
using Ninject.Modules;

namespace Example.Wiring
{
    public static class KernelFactory
    {

        static KernelFactory()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static IKernel ConfigureKernel()
        {
            var asm = Assembly.GetExecutingAssembly();
            var modules = asm.GetTypes()
                .Where(t => typeof(NinjectModule).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<NinjectModule>()
                .ToArray();
            return new StandardKernel(modules);
        }

    }
}
