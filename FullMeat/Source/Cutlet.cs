// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo

/* CutletAction.cs -- мясное блюдо: котлета
 * Ars Magna project, http://arsmagna.ru
 */

#region Using directives

using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

#endregion

#nullable enable

namespace FullMeat
{
    /// <summary>
    /// Мясное блюдо: котлета.
    /// </summary>
    public class Cutlet
        : IMeatDish
    {
        #region Construction

        public Cutlet
            (
                IOptions<MeatOptions> options, 
                ILogger<Cutlet> logger
            )
        {
            _options = options.Value;
            _logger = logger;
        }

        #endregion

        #region Private members

        private readonly MeatOptions _options;
        private readonly ILogger _logger;

        #endregion
        
        #region IMeatAction members

        /// <inheritdoc cref="IMeatDish.PrepareDishAsync"/>
        public Task PrepareDishAsync
            (
                DishBrand brand
            )
        {
            var name = _options.Name;
            var weight = _options.Weight;
            _logger.LogInformation($"Cutlet is dish: {brand.Title} of {name}, {weight} kg");
            
            return Task.CompletedTask;
        }

        #endregion
    }
}
