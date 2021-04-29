// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo

/* IMeatDish.cs -- интерфейс приготовления мясного блюда
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using System.Threading.Tasks;

#endregion

#nullable enable

namespace FullMeat
{
    /// <summary>
    /// Интерфейс приготовления мясного блюда.
    /// </summary>
    public interface IMeatDish
    {
        /// <summary>
        /// Приготовление некоего блюда.
        /// </summary>
        Task PrepareDishAsync(DishBrand brand);
    }
}
