using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.UI.CSS.Rendering.Text
{
    public interface ITextTransformation
    {
        /// <summary>
        /// Applies the text transform to the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        string Apply(string text);
    }
}
