// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo

/* ServerConfiguration.cs -- конфигурация TCP-сервера
 * Ars Magna project, http://arsmagna.ru
 */

#nullable enable

namespace ModernTcpServer
{
    /// <summary>
    /// Конфигурация TCP-сервера
    /// </summary>
    public sealed class ServerConfiguration
    {
        #region Properties

        /// <summary>
        /// Номер порта для прослушивания.
        /// </summary>
        public short Port { get; set; }

        #endregion
        
    } // class ServerConfiguration
    
} // namespace ModernTcpServer
