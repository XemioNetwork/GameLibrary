using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Game.Scenes.Transitions
{
    public class ColorTransition : ITransition
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorTransition"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        public ColorTransition(Color color)
        {
            this.Color = color;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color Color { get; private set; }
        #endregion

        #region Implementation of ITransition
        /// <summary>
        /// Gets a value indicating whether the transition is completed.
        /// </summary>
        public bool IsCompleted { get; private set; }
        /// <summary>
        /// Gets or sets the current scene.
        /// </summary>
        Scene ITransition.Current { get; set; }
        /// <summary>
        /// Gets or sets the next scene.
        /// </summary>
        Scene ITransition.Next { get; set; }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
        }
        /// <summary>
        /// Renders a transition between the specified scenes.
        /// </summary>
        /// <param name="current">The current scenes frame.</param>
        /// <param name="next">The next scenes frame.</param>
        public void Render(IRenderTarget current, IRenderTarget next)
        {
        }
        #endregion
    }
}
