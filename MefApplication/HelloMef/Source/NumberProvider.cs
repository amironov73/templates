using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

using Microsoft.Extensions.Logging;

using Common;

namespace HelloMef
{
    public class NumberProvider
        : INumberProvider
    {
        private readonly ILogger _logger;

        public NumberProvider
            (
                ILogger<NumberProvider> logger
            )
        {
            _logger = logger;

            Compose();
        }

        private static bool HaveGetNumber(Assembly assembly)
        {
            return assembly.GetTypes()
                .Any
                    (
                        p => typeof(IGetNumber).IsAssignableFrom(p)
                    );
        }

        private void Compose()
        {
            var assemblies = new List<Assembly> { typeof(Program).Assembly };
            var pluginPath = Path.Combine(AppContext.BaseDirectory, "plugins");
            var pluginAssemblies
                = Directory.GetFiles(pluginPath, "*.dll")
                    .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                    .Where(HaveGetNumber)
                    .ToArray();
            assemblies.AddRange(pluginAssemblies);
            _logger.LogInformation($"Found assemblies: {pluginAssemblies.Length}");

            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies);

            using var container = configuration.CreateContainer();
            Services = container.GetExports<IGetNumber>();
        }

        [ImportMany]
        public IEnumerable<IGetNumber> Services { get; private set; }

        public IEnumerable<GotNumber> GetNumbers()
        {
            foreach (var service in Services)
            {
                var id = service.GetType().Name;
                var value = service.GetNumber();
                var item = new GotNumber {Id = id, Value = value};
                yield return item;
            }
        }
    }
}
