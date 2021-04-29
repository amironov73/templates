// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo

/* Program.cs -- точка входа в программу
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

#endregion

#nullable enable

namespace FullMeat
{
    class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        static async Task<int> Main
            (
                string[] args
            )
        {
            // Включаем конфигурирование в appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var rootCommand = new RootCommand("FullMeat")
            {
                new Option<string>("--brand", () => "Kiev")
            };

            var parseResult = new CommandLineBuilder(rootCommand)
                .UseDefaults()
                .Build()
                .Parse(args);
            var brandTitle = parseResult.ValueForOption<string>("--brand");
            var brand = new DishBrand { Title = brandTitle };

            // Настраиваем хост
            var builder = Host.CreateDefaultBuilder(args).ConfigureServices
            (
                services =>
                {
                    services.AddOptions();
                    
                    // включаем логирование
                    services.AddLogging(logging => logging.AddConsole());
                    
                    // регистрируем интерфейсы
                    services.AddTransient<IMeatDish, Cutlet>();
                    
                    // регистрируем наш сервис
                    services.AddSingleton(brand);
                    services.AddHostedService<MeatService>();

                    // откуда брать настройки
                    var section = config.GetSection("MeatOptions");
                    services.Configure<MeatOptions>(section);
                }
            );

            using var host = builder.Build();
            
            await host.StartAsync(); // запускаем наш сервис
            await host.WaitForShutdownAsync(); // ожидаем его окончания

            // К этому моменту всё успешно выполнено либо произошла ошибка
            Console.WriteLine("THAT'S ALL, FOLKS!");

            return 0;
        }
    }
}
