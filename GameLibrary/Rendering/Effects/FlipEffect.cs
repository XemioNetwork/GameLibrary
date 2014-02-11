using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Rendering.Effects
{
    public class FlipEffect : IEffect
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FlipEffect" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public FlipEffect(FlipEffectOptions options)
        {
            this.Options = options;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the options.
        /// </summary>
        public FlipEffectOptions Options { get; private set; }
        #endregion
    }
}
