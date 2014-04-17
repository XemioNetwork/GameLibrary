using System;
using System.Windows.Forms;
using Xemio.GameLibrary.Rendering.Surfaces;

namespace Xemio.GameLibrary.Rendering.GdiPlus
{
    using Drawing2D = System.Drawing.Drawing2D;

    public class ManagedGdiRenderManager : GdiRenderManager
    {
        #region Fields
        private bool _hasRegisteredPaintHandler = false;
        #endregion

        #region Overrides of GdiRenderManager
        /// <summary>
        /// Presents this instance. 
        /// </summary>
        public override void Present()
        {       
            var surface = XGL.Components.Require<WindowSurface>();

            if (surface.Control == null)
                return;

            Control control = surface.Control;
            if (!this._hasRegisteredPaintHandler)
            {
                control.Paint += (sender, e) => {                
                    var backBuffer = (GdiRenderTarget)this.GraphicsDevice.BackBuffer;
                    var bitmap = backBuffer.Bitmap;
    
                    e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed;
                    e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor;
                    e.Graphics.CompositingMode = Drawing2D.CompositingMode.SourceCopy;
                    e.Graphics.CompositingQuality = Drawing2D.CompositingQuality.AssumeLinear;

                    e.Graphics.DrawImage(bitmap, 0, 0, surface.Width, surface.Height);
                };
    
                this._hasRegisteredPaintHandler = true;
            }
    
            control.Invoke((Action)(control.Refresh));

            base.Present();
        }
        #endregion
    }
}

