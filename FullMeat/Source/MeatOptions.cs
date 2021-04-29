// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo
// ReSharper disable UnusedAutoPropertyAccessor.Global

/* MeatOptions.cs -- опции для сервиса
 * Ars Magna project, http://arsmagna.ru
 */

#nullable enable

namespace FullMeat
{
    /// <summary>
    /// Опции для сервиса по приготовлению мясных блюд.
    /// </summary>
    public class MeatOptions
    {
        /// <summary>
        /// Название мяса.
        /// </summary>
        public string? Name { get; set; }
        
        /// <summary>
        /// Вес.
        /// </summary>
        public double Weight { get; set; }
    }
}
