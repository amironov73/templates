// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable UnusedAutoPropertyAccessor.Global

/* PersonData.cs -- данные о некоторой персоне
 * Ars Magna project, http://arsmagna.ru
 */

#nullable enable

namespace FormsApp
{
    /// <summary>
    /// Данные о некоторой персоне.
    /// </summary>
    public sealed class PersonData
    {
        public string? Name { get; set; }
        public string? Age { get; set; }
    }
}
