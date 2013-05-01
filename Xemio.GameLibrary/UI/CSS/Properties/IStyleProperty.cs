using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.UI.CSS.Rendering;

namespace Xemio.GameLibrary.UI.CSS.Properties
{
    public interface IStyleProperty : ILinkable<string>
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is important.
        /// </summary>
        bool IsImportant { get; set; }
        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        void Parse(string value);
        /// <summary>
        /// Appends the specified property to the render state.
        /// </summary>
        /// <param name="state">The state.</param>
        void Append(WidgetRenderState state);
    }
}
