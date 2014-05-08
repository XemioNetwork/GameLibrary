using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Rendering.Effects.Processors
{
    public class ScaleEffectProcessor : EffectProcessor<ScaleEffect>
    {
        #region Overrides of EffectProcessor<ScaleEffect>
        /// <summary>
        /// Enables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        protected override void Enable(ScaleEffect effect, GraphicsDevice graphicsDevice)
        {
        }
        /// <summary>
        /// Disables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        protected override void Disable(ScaleEffect effect, GraphicsDevice graphicsDevice)
        {
        }
        #endregion
    }
}
