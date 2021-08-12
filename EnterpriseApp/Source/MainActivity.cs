// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo

/* MainActivity.cs -- основная активность приложения
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using System.CommandLine.Parsing;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

#endregion

#nullable enable

namespace EnterpriseApp
{
    /// <summary>
    /// Основная активность приложения.
    /// Вызывает нужные сервисы, после чего завершает работу приложения.
    /// </summary>
    public sealed class MainActivity
        : BackgroundService
    {
        private readonly IGreeter _greeter;
        private readonly IHostApplicationLifetime _lifetime;
        private readonly IConfigurationRoot _configuration;
        private readonly ILogger _logger;
        private readonly SharedResource _sharedResource;
        private readonly ParseResult _parseResult;

        public MainActivity
            (
                IGreeter greeter,
                IHostApplicationLifetime lifetime,
                IConfigurationRoot configuration,
                ILogger<MainActivity> logger,
                SharedResource sharedResource,
                ParseResult parseResult
            )
        {
            _greeter = greeter;
            _lifetime = lifetime;
            _configuration = configuration;
            _logger = logger;
            _sharedResource = sharedResource;
            _parseResult = parseResult;
        }

        #region BackgroundService members

        protected override async Task ExecuteAsync
            (
                CancellationToken stoppingToken
            )
        {
            _logger.LogInformation(_sharedResource.EnterpriseApplication);
            
            // получаем результат разбора командной строки
            var brand = _parseResult.ValueForOption<string>("--brand");
            _logger.LogInformation($"Brand: {brand}");
            
            // получаем данные из конфигурации
            var personData = new PersonData();
            _configuration.Bind("PersonData", personData);
            
            // вызываем сервис, умеющий приветствовать пользователя
            await _greeter.GreetAsync(personData.Name!, personData.Age!, stoppingToken);
            
            // гасим приложение
            _lifetime.StopApplication();
        }

        #endregion
    }
}
