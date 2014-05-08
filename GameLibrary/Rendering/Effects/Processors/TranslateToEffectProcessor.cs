using System.Collections.Generic;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Effects.Processors
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
        /// <param name="graphicsDevice">The graphics device.</param>
        protected override void Enable(TranslateToEffect effect, GraphicsDevice graphicsDevice)
        {
            var translateProcessor = this.Find<TranslateEffectProcessor>();
            this._previousPositions.Push(translateProcessor.Offset);

            translateProcessor.Offset = effect.Position;
        }
        /// <summary>
        /// Disables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        protected override void Disable(TranslateToEffect effect, GraphicsDevice graphicsDevice)
        {
            var translateProcessor = this.Find<TranslateEffectProcessor>();
            translateProcessor.Offset = this._previousPositions.Pop();
        }
        #endregion
    }
}
