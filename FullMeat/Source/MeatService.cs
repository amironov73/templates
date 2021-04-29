// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedParameter.Local

/* MeatService.cs -- сервис по приготовлению блюд из мяса
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

#endregion

#nullable enable

namespace FullMeat
{
    internal sealed class MeatService
        : BackgroundService
    {
        #region Construction

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        /// <param name="lifetime">Срок жизни.</param>
        /// <param name="dish">Действие по приготовлению блюда.</param>
        /// <param name="host">Хост (для доступа к сервисам).</param>
        public MeatService
            (
                ILogger<MeatService> logger,
                IHostApplicationLifetime lifetime,
                IMeatDish dish,
                IHost host
            )
        {
            _logger = logger;
            _lifetime = lifetime;
            _dish = dish;
            _host = host;
        }

        #endregion

        #region Private members

        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _lifetime;
        private readonly IMeatDish _dish;
        private readonly IHost _host;

        #endregion

        #region BackgroundService members

        protected override async Task ExecuteAsync
            (
                CancellationToken stoppingToken
            )
        {
            using var scope = _logger.BeginScope("FreshMeat");

            _logger.LogInformation("Starting");

            // Получаем данные о бренде
            var brand = _host.Services.GetRequiredService<DishBrand>();
            
            // Собственно действия по приготовлению блюда
            await _dish.PrepareDishAsync(brand);
            
            _logger.LogInformation("Stopping");
            _lifetime.StopApplication();
        }

        #endregion
    }
}
