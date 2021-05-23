// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo

/* ClientHandler.cs -- реализация обработчика клиента
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

#endregion

#nullable enable

namespace ModernTcpServer
{
    /// <summary>
    /// Реализация обработчика клиента.
    /// </summary>
    public sealed class ClientHandler
        : IClientHandler
    {
        #region Construction

        public ClientHandler
            (
                ILogger<ClientHandler> logger
            )
        {
            _logger = logger;
        }

        #endregion

        #region Private members

        private readonly ILogger _logger;

        #endregion

        #region IClientHandler members

        /// <inheritdoc cref="IClientHandler.HandleClientAsync"/>
        public async Task HandleClientAsync
            (
                TcpClient client, 
                CancellationToken stoppingToken
            )
        {
            using var scope = _logger.BeginScope("HandleClient");
            
            var encoding = Encoding.UTF8;
            var stream = client.GetStream();
            var buffer = new byte[4000];
            var size = await stream.ReadAsync(buffer, 0, buffer.Length, stoppingToken);
            var text = encoding.GetString(buffer, 0, size);
            _logger.LogInformation($"Received: {text}");
            text = "ACK " + text + "\r\n";
            buffer = encoding.GetBytes(text);
            await stream.WriteAsync(buffer, 0, buffer.Length, stoppingToken);
            await stream.FlushAsync(stoppingToken);
            _logger.LogInformation("ACK send OK");
            
            // даем данным дойти
            await Task.Delay(2000, stoppingToken);
            
            client.Close();
            
        } // method HandleClientAsync

        #endregion
        
    } // class ClientHandler
    
} // namespace ModernTcpClient
