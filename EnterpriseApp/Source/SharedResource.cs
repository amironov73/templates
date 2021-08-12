// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

/* SharedResource.cs -- общие локализуемые ресурсы
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using Microsoft.Extensions.Localization;

#endregion

#nullable enable

namespace EnterpriseApp
{
    /// <summary>
    /// Здесь помещаются общие для приложения ресурсы,
    /// подлежащие локализации, (например, строки сообщений).
    /// </summary>
    public class SharedResource
    {
        private readonly IStringLocalizer _localizer;
 
        public SharedResource
            (
                IStringLocalizer<SharedResource> localizer
            )
        {
            _localizer = localizer;
        }
 
        public string EnterpriseApplication => _localizer["Enterprise application"]!;
    }
}
