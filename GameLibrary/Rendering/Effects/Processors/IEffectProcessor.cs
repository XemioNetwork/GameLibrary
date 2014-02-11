using System;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.GameLibrary.Rendering.Effects.Processors
{
    [ManuallyLinked]
    public interface IEffectProcessor : ILinkable<Type>
    {
        /// <summary>
        /// Enables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="renderManager">The render manager.</param>
        void Enable(IEffect effect, IRenderManager renderManager);
        /// <summary>
        /// Disables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="renderManager">The render manager.</param>
        void Disable(IEffect effect, IRenderManager renderManager);
    }
}
