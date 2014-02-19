using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Effects;
using Xemio.GameLibrary.Rendering.Effects.Processors;

namespace Xemio.GameLibrary.Rendering.GdiPlus.Processors
{
    public class TranslateToEffectProcessor : EffectProcessor<TranslateToEffect>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateToEffectProcessor"/> class.
        /// </summary>
        public TranslateToEffectProcessor()
        {
            this._previousPositions = new Stack<Vector2>();
        }
        #endregion

        #region Fields
        private readonly Stack<Vector2> _previousPositions; 
        #endregion

        #region Overrides of EffectProcessor<TranslateToEffect>
        /// <summary>
        /// Enables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="renderManager">The render manager.</param>
        protected override void Enable(TranslateToEffect effect, IRenderManager renderManager)
        {
            var gdiRenderManager = (GdiRenderManager)renderManager;
            this._previousPositions.Push(gdiRenderManager.Offset);

            gdiRenderManager.Offset = effect.Position;
        }
        /// <summary>
        /// Disables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="renderManager">The render manager.</param>
        protected override void Disable(TranslateToEffect effect, IRenderManager renderManager)
        {
            var gdiRenderManager = (GdiRenderManager)renderManager;
            gdiRenderManager.Offset = this._previousPositions.Pop();
        }
        #endregion
    }
}
