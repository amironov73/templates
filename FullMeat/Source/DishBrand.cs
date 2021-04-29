// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo
// ReSharper disable PropertyCanBeMadeInitOnly.Global

/* DishBrand.cs -- бренд мясного блюда
 * Ars Magna project, http://arsmagna.ru
 */

#nullable enable

namespace FullMeat
{
    /// <summary>
    /// Бренд мясного блюда.
    /// </summary>
    public sealed class DishBrand
    {
        #region Properties

        /// <summary>
        /// Наименование бренда.
        /// </summary>
        public string? Title { get; set; }

        #endregion
    }
}
