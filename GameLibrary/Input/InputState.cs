using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Input
{
    public class InputState
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InputState"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public InputState(float value) : this(true, value)
        {
        }
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
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this <see cref="InputState"/> is active.
        /// </summary>
        public bool Active { get; private set; }
        /// <summary>
        /// Gets the value.
        /// </summary>
        public float Value { get; private set; }
        #endregion

        #region States
        /// <summary>
        /// Gets the pressed state.
        /// </summary>
        public static InputState Pressed
        {
            get{ return new InputState(true, 1.0f); }
        }
        /// <summary>
        /// Gets the released state.
        /// </summary>
        public static InputState Released
        {
            get { return new InputState(false, 0.0f); }
        }
        #endregion
    }
}
