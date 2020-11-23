using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HelloMef
{
    class Program
    {
        static void GetNumbers(IHost host)
        {
            var provider = host.Services.GetRequiredService<INumberProvider>();

            foreach (var number in provider.GetNumbers())
            {
                Console.WriteLine($"{number.Id,-7} => {number.Value}");
            }
        }

        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddTransient<INumberProvider, NumberProvider>();
                })
                .Build();

            using (host)
            {
                GetNumbers(host);
            }
        }
    }
}
