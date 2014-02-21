using System;
using Xemio.GameLibrary.Rendering.Surfaces;
using System.Drawing;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.GdiPlus
{
    public class NativeGdiRenderManager : GdiRenderManager
    {
        #region Overrides of GdiRenderManager
        /// <summary>
        /// Presents this instance. 
        /// </summary>
        public override void Present()
        {
            var surface = XGL.Components.Require<WindowSurface>();

            if (surface.Control == null)
                return;

            Graphics graphics = surface.Control.CreateGraphics();
    
            var backBuffer = (GdiRenderTarget)this.GraphicsDevice.BackBuffer;
            var bitmap = backBuffer.Bitmap;

            IntPtr hdc = graphics.GetHdc();
            IntPtr dc = Gdi.CreateCompatibleDC(hdc);
            IntPtr buffer = bitmap.GetHbitmap();

            Gdi.SelectObject(dc, buffer);

            Gdi.StretchBlt
            (
                hdc, 0, 0,
                surface.Width,
                surface.Height,
                dc,
                0, 0,
                this.GraphicsDevice.DisplayMode.Width, 
                this.GraphicsDevice.DisplayMode.Height,
                GdiRasterOperations.SRCCOPY
            );

            Gdi.DeleteObject(buffer);
            Gdi.DeleteObject(dc);

            graphics.ReleaseHdc(hdc);

            base.Present();
        }
        #endregion
    }
}

