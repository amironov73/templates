// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo

/* TcpService.cs -- эхо-сервис
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

#endregion

#nullable enable

namespace ModernTcpServer
{
    /// <summary>
    /// Эхо-сервис.
    /// </summary>
    public sealed class TcpService
        : BackgroundService
    {
        #region Construction

        /// <summary>
        /// Конструктор.
        /// </summary>
        public TcpService
            (
                ILogger<TcpService> logger,
                IHost host,
                IOptions<ServerConfiguration> serverConfiguration
            )
        {
            _logger = logger;
            _host = host;
            var configuration = serverConfiguration.Value;

            _listener = new TcpListener
                (
                    IPAddress.Any, 
                    configuration.Port
                );
            _listener.Start();
        }

        #endregion

        #region Private members

        private readonly ILogger _logger;
        private readonly TcpListener _listener;
        private readonly IHost _host;

        /// <summary>
        /// Обрабатываем клиента.
        /// </summary>
        private async Task HandleAsync
            (
                TcpClient client,
                CancellationToken stoppingToken
            )
        {
            var handler = _host.Services.GetRequiredService<IClientHandler>();
            
            await handler.HandleClientAsync(client, stoppingToken);
            
        } // method HandleAsync

        #endregion

        #region BackgroundService members

        /// <inheritdoc cref="BackgroundService.ExecuteAsync"/>
        protected override async Task ExecuteAsync
            (
                CancellationToken stoppingToken
            )
        {
            using var scope = _logger.BeginScope("ExecuteAsync");
            
            _logger.LogInformation("Starting");

            await using (stoppingToken.Register(() => _listener.Stop()))
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var client = await _listener.AcceptTcpClientAsync().ConfigureAwait(false);
                        _logger.LogInformation($"Got client: {client.Client.RemoteEndPoint}");
                        
                        #pragma warning disable 4014
                        
                        // Запустил-и-забыл
                        
                        Task.Factory.StartNew
                                (
                                    async () => await HandleAsync(client, stoppingToken),
                                    stoppingToken,
                                    TaskCreationOptions.LongRunning,
                                    TaskScheduler.Current
                                )
                            
                            .ConfigureAwait(false);
                        
                        #pragma warning restore 4014
                    }
                    catch (InvalidOperationException)
                    {
                        _logger.LogError("InvalidOperationException");
                        
                        // Either tcpListener.Start wasn't called (a bug!)
                        // or the CancellationToken was cancelled before
                        // we started accepting (giving an InvalidOperationException),
                        // or the CancellationToken was cancelled after
                        // we started accepting (giving an ObjectDisposedException).
                        //
                        // In the latter two cases we should surface the cancellation
                        // exception, or otherwise rethrow the original exception.
                        stoppingToken.ThrowIfCancellationRequested();
                        throw;                        
                    }
                    catch (OperationCanceledException)
                    {
                        _logger.LogInformation("OperationCanceledException");
                        break;
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError($"{exception.Message}");
                        break;
                    }
                }
            }

            _logger.LogInformation("Stopping");
            
        } // method ExecuteAsync

        #endregion

    } // class TcpService
    
} // namespace ModernTcpServer
