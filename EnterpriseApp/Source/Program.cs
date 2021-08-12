// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CommentTypo
// ReSharper disable LocalizableElement
// ReSharper disable StringLiteralTypo

/* Program.cs -- точка входа в программу
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

#endregion

#nullable enable

namespace EnterpriseApp
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
            try
            {
                // Включаем конфигурирование в appsettings.json
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables()
                    .AddCommandLine(args)
                    .Build();

                var parseResult = ParseCommandLine(args);
                using var host = CreateHostBuilder(args, config, parseResult).Build();

                await host.StartAsync(); // запускаем наш сервис
                await host.WaitForShutdownAsync(); // ожидаем его окончания
            }
            catch (Exception exception)
            {
                await Console.Error.WriteLineAsync(exception.ToString());
                
                return 1;
            }

            // К этому моменту всё успешно выполнено либо произошла ошибка
            await Console.Out.WriteLineAsync("THAT'S ALL, FOLKS!");

            return 0;
        }

        /// <summary>
        /// Разбор командной строки.
        /// </summary>
        static ParseResult ParseCommandLine
            (
                string[] args
            )
        {
            var rootCommand = new RootCommand("Enterprise Application")
            {
                new Option<string>("--brand", () => "Ars Magna")
            };

            var result = new CommandLineBuilder(rootCommand)
                .UseDefaults()
                .Build()
                .Parse(args);

            return result;
        }

        /// <summary>
        /// Создание хоста приложения.
        /// </summary>
        static IHostBuilder CreateHostBuilder
            (
                string[] args, 
                IConfigurationRoot config, 
                ParseResult parseResult
            ) 
                => Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
            {
                services.AddOptions();
                services.AddLocalization();
                
                // включаем логирование
                services.AddLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddNLog(config);
                });
                    
                // регистрируем интерфейсы
                services.AddTransient<IGreeter, InternationalGreeter>();
                
                // откуда брать настройки
                services.AddSingleton<SharedResource>();
                services.AddSingleton(parseResult);
                services.AddSingleton(config);
                
                // регистрируем наш сервис
                services.AddHostedService<MainActivity>();
                
            });

    }
}
