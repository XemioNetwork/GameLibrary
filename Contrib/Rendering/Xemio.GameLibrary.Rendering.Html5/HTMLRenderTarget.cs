using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.HTML5
{
    public class HTMLRenderTarget : HTMLTexture, IRenderTarget
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HTMLRenderTarget"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public HTMLRenderTarget(int width, int height)
        {
            WebSurface surface = XGL.Components.Require<WebSurface>();

            dynamic element = surface.Document.createElement("canvas");
            element.style = "display: none;";
            element.width = width;
            element.height = height;
                
            this.Texture = element;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the context.
        /// </summary>
        public dynamic Context
        {
            get { return this.Texture.getContext("2d"); }
        }
        #endregion
    }
}
