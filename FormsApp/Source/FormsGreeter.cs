// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

/* FormsGreeter.cs -- умеет приветствовать пользователя с помощью WinForms
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

#endregion

#nullable enable

namespace FormsApp
{
    /// <summary>
    /// Умеет приветствовать пользователя с помощью WinForms.
    /// </summary>
    public sealed class FormsGreeter
        : IGreeter
    {
        private readonly ILogger _logger;
        private readonly IStringLocalizer _localizer;

        public FormsGreeter
            (
                ILogger<FormsGreeter> logger,
                IStringLocalizer<FormsGreeter> localizer
            )
        {
            _logger = logger;
            _localizer = localizer;
        }

        public Task GreetAsync
            (
                string person, 
                string age,
                CancellationToken token
            )
        {
            _logger.LogInformation("GreetAsync enter");
            
            var hello = _localizer["Hello"];
            var message = $"{hello} {person} ({age})";

            MessageBox.Show(message);
            
            _logger.LogInformation("GreetAsync leave");
            
            return Task.CompletedTask;
        }
    }
}
