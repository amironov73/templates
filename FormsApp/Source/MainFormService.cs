// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

/* MainFormService.cs -- сервис, создающий и показывающий главную форму приложения
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

#endregion

#nullable enable

namespace FormsApp
{
    /// <summary>
    /// Сервис, создающий и показывающий главную форму приложения.
    /// </summary>
    public sealed class MainFormService
        : BackgroundService
    {
        private readonly IHost _host;
        private readonly ILogger _logger;

        public MainFormService
            (
                IHost host
            )
        {
            _host = host;
            _logger = host.Services.GetRequiredService<ILogger<MainFormService>>();
        }

        #region BackgroundService members

        protected override Task ExecuteAsync
            (
                CancellationToken stoppingToken
            )
        {
            _logger.LogInformation("Before Application::Run");
            
            using var mainForm = (Form) _host.Services.GetRequiredService<IMainForm>();
            
            Application.Run(mainForm);
            _logger.LogInformation("After Application::Run");

            var appLifetime = _host.Services.GetRequiredService<IHostApplicationLifetime>();
            appLifetime.StopApplication();
            
            return Task.CompletedTask;
        }

        #endregion
    }
}
