using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Loader;

using Autofac;
using Autofac.Configuration;

using Interfaces;

using Microsoft.Extensions.Configuration;

namespace SuperApp
{
    class Program
    {
        static void Main()
        {
            AssemblyLoadContext.Default.Resolving += (context, asm) =>
            {
                var path = Path.Combine
                    (
                        AppContext.BaseDirectory,
                        asm.Name + ".dll"
                    );
                return context.LoadFromAssemblyPath(path);
            };

            var config = new ConfigurationBuilder();
            config.AddJsonFile("autofac.json");
            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);
            var container = builder.Build();

            var hellos = container.Resolve<IEnumerable<IHello>>();
            foreach (var hello in hellos)
            {
                hello.SayHello("World");
            }
        }
    }
}
