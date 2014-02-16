using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Rendering.Effects;
using Xemio.GameLibrary.Rendering.Effects.Processors;

namespace Xemio.GameLibrary.Rendering.GdiPlus.Processors
{
    public class TranslateEffectProcessor : EffectProcessor<TranslateEffect>
    {
        #region Overrides of EffectProcessor<TranslateEffect>
        /// <summary>
        /// Enables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="renderManager">The render manager.</param>
        protected override void Enable(TranslateEffect effect, IRenderManager renderManager)
        {
            ((GdiRenderManager)renderManager).Offset += effect.Offset;
        }
        /// <summary>
        /// Disables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="renderManager">The render manager.</param>
        protected override void Disable(TranslateEffect effect, IRenderManager renderManager)
        {
            ((GdiRenderManager)renderManager).Offset -= effect.Offset;
        }
        #endregion
    }
}
