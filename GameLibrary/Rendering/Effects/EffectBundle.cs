using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Rendering.Effects
{
    public abstract class EffectBundle : IEffect
    {
        #region Properties
        /// <summary>
        /// Gets the effects.
        /// </summary>
        public abstract IEnumerable<IEffect> Effects { get; } 
        #endregion
    }
}
