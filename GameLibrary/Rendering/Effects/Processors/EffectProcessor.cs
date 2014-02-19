using System;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.GameLibrary.Rendering.Effects.Processors
{
    [ManuallyLinked]
    public abstract class EffectProcessor<T> : IEffectProcessor where T : IEffect
    {
        #region Methods
        /// <summary>
        /// Enables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="renderManager">The render manager.</param>
        protected abstract void Enable(T effect, IRenderManager renderManager);
        /// <summary>
        /// Disables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="renderManager">The render manager.</param>
        protected abstract void Disable(T effect, IRenderManager renderManager);
        #endregion

        #region Implementation of IEffectProcessor
        /// <summary>
        /// Enables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="renderManager">The render manager.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void IEffectProcessor.Enable(IEffect effect, IRenderManager renderManager)
        {
            this.Enable((T)effect, renderManager);
        }
        /// <summary>
        /// Disables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="renderManager">The render manager.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void IEffectProcessor.Disable(IEffect effect, IRenderManager renderManager)
        {
            this.Disable((T)effect, renderManager);
        }
        #endregion

        #region Implementation of ILinkable<Type>
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public Type Id
        {
            get { return typeof(T); }
        }
        #endregion
    }
}
