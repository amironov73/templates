// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo

/* IClientHandler.cs -- интерфейс обработчика клиента для эхо-сервиса
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

#endregion

#nullable enable

namespace ModernTcpServer
{
    /// <summary>
    /// Интерфейс обработчика клиентов для эхо-сервиса
    /// </summary>
    public interface IClientHandler
    {
        /// <summary>
        /// Обработка клиента.
        /// </summary>
        Task HandleClientAsync(TcpClient client, CancellationToken stoppingToken);
        
    } // interface IClientHandler
    
} // namespace ModernTcpServer
