// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

/* IGreeter.cs -- интерфейс сервиса, умеющего приветствовать пользователя
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using System.Threading;
using System.Threading.Tasks;

#endregion

#nullable enable

namespace FormsApp
{
    /// <summary>
    /// Интерфейс сервиса, умеющего приветствовать пользователя.
    /// </summary>
    public interface IGreeter
    {
        Task GreetAsync
            (
                string person, 
                string age, 
                CancellationToken token
            );
        
    }
}
