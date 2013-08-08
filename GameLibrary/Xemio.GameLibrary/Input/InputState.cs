using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Input
{
    public class InputState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputState"/> class.
        /// </summary>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <param name="value">The value.</param>
        public InputState(bool active, float value)
        {
            this.Active = active;
            this.Value = value;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="InputState"/> is active.
        /// </summary>
        public bool Active { get; private set; }
        /// <summary>
        /// Gets the value.
        /// </summary>
        public float Value { get; private set; }

        #region Empty
        private static readonly InputState _empty = new InputState(false, 0.0f);
        /// <summary>
        /// The empty
        /// </summary>
        public static InputState Empty
        {
            get { return InputState._empty; }
        }
        #endregion Empty
    }
}
