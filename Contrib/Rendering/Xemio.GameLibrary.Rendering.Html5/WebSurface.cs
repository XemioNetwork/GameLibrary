using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using JSIL;
using Xemio.GameLibrary.Rendering.Surfaces;

namespace Xemio.GameLibrary.Rendering.HTML5
{
    public class WebSurface : ISurface
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WebSurface"/> class.
        /// </summary>
        /// <param name="canvasId">The canvas id.</param>
        public WebSurface(string canvasId)
        {
            this._canvasId = canvasId;
        }
        #endregion

        #region Fields
        private readonly string _canvasId;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the document.
        /// </summary>
        public dynamic Document
        {
            get { return Builtins.Global["document"]; }
        }
        /// <summary>
        /// Gets the canvas.
        /// </summary>
        public dynamic Canvas
        {
            get { return this.Document.getElementById(this._canvasId); }
        }
        #endregion
        
        #region Implementation of ISurface
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; private set; }
        #endregion
    }
}
