using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.UI.CSS.Rendering;

namespace Xemio.GameLibrary.UI.CSS.Properties
{
    public class ColorProperty : IStyleProperty
    {
        #region Properties
        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color Color { get; private set; }
        #endregion

        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public string Identifier { get; private set; }
        #endregion

        #region Implementation of IStyleProperty
        /// <summary>
        /// Gets or sets a value indicating whether this instance is important.
        /// </summary>
        public bool IsImportant { get; set; }
        /// <summary>
        /// Initializes the property.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Parse(string value)
        {
            //TODO: parse color.
        }
        /// <summary>
        /// Appends the specified property to the render state.
        /// </summary>
        /// <param name="state">The state.</param>
        public void Append(WidgetRenderState state)
        {
            state.ForeColor = this.Color;
        }
        #endregion
    }
}
