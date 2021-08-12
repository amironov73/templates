// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

/* InternationalGreeter.cs -- умеет приветствовать на разных языках
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

#endregion

namespace EnterpriseApp
{
    /// <summary>
    /// Реализация сервиса <see cref="IGreeter"/>,
    /// умеющая приветствовать пользователя на разных языках.
    /// </summary>
    public sealed class InternationalGreeter
        : IGreeter
    {
        private readonly ILogger _logger;
        private readonly IStringLocalizer _localizer;

        public InternationalGreeter
            (
                ILogger<InternationalGreeter> logger,
                IStringLocalizer<InternationalGreeter> localizer
            )
        {
            _logger = logger;
            _localizer = localizer;
        }

        public async Task GreetAsync
            (
                string person, 
                string age,
                CancellationToken token
            )
        {
            _logger.LogInformation("GreetAsync enter");
            
            var hello = _localizer["Hello"];
            var message = $"{hello} {person} ({age})";
            
            await Console.Out.WriteLineAsync(message.AsMemory(), token);
            
            _logger.LogInformation("GreetAsync leave");
        }
    }
}
