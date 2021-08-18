// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable LocalizableElement
// ReSharper disable StringLiteralTypo

/* Program.cs -- инициализация и точка входа в программу
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Windows.Forms;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

#endregion

#nullable enable

namespace FormsApp
{
    /// <summary>
    /// Инициализация и точка входа в программу.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Собственно точка входа в программу.
        /// </summary>
        [STAThread]
        static void Main
            (
                string[] args
            )
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
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
                var hostBuilder = CreateHostBuilder(args, config, parseResult);
                LoadPlugins(hostBuilder);
                using var host = hostBuilder.Build();
                
                host.Run();
            }
            catch (Exception exception)
            {
                MessageBox.Show
                    (
                        exception.ToString(), 
                        "Exception", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error
                    );
            }
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
        /// Загружаем плагины.
        /// </summary>
        static void LoadPlugins
            (
                IHostBuilder hostBuilder
            )
        {
            var assemblies = new List<Assembly> { typeof(Program).Assembly };
            var pluginPath = Path.Combine(AppContext.BaseDirectory, "plugins");
            if (Directory.Exists(pluginPath))
            {
                var pluginAssemblies = Directory
                    .GetFiles(pluginPath, "*plugin.dll")
                    .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                    .ToArray();
                assemblies.AddRange(pluginAssemblies);
            }

            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies);

            var container = configuration.CreateContainer();
            hostBuilder.ConfigureServices(services => services.AddSingleton(container));
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
                    services.AddTransient<IMainForm, MainForm>();
                    services.AddTransient<IGreeter, FormsGreeter>();
                
                    // откуда брать настройки
                    services.AddSingleton<SharedResource>();
                    services.AddSingleton(parseResult);
                    services.AddSingleton(config);
                
                    // регистрируем сервисы
                    services.AddHostedService<MainFormService>();
                
                });
    }
}
