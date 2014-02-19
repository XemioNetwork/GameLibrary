using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Fonts
{
    public interface ITextRasterizer
    {
        /// <summary>
        /// Renders the specified text.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        void Render(IFont font, string text, Vector2 position);
    }
}
