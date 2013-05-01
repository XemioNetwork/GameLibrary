using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.UI.CSS.Rendering.Text
{
    public class UppercaseTransform : ITextTransformation
    {
        #region Implementation of ITextTransformation
        /// <summary>
        /// Applies the text transform to the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        public string Apply(string text)
        {
            return text.ToUpper();
        }
        #endregion
    }
}
