using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Rendering.Xna
{
    using XemioGraphicsDevice = Xemio.GameLibrary.Rendering.GraphicsDevice;
    using GraphicsDevice = Microsoft.Xna.Framework.Graphics.GraphicsDevice;

    internal static class XnaHelper
    {
        #region Constructors
        /// <summary>
        /// Initializes the <see cref="XnaHelper"/> class.
        /// </summary>
        static XnaHelper()
        {
            XnaHelper.CreateDevice();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public static GraphicsDevice GraphicsDevice { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new graphics device instance.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public static void CreateDevice()
        {
            PresentationParameters parameters = new PresentationParameters();
            XemioGraphicsDevice graphicsDevice = XGL.GetComponent<XemioGraphicsDevice>();

            parameters.BackBufferWidth = (int)MathHelper.Max(graphicsDevice.DisplayMode.Width, 1);
            parameters.BackBufferHeight = (int)MathHelper.Max(graphicsDevice.DisplayMode.Height, 1);
            parameters.BackBufferFormat = SurfaceFormat.Color;
            parameters.DepthStencilFormat = DepthFormat.Depth24;
            parameters.DeviceWindowHandle = graphicsDevice.Handle;
            parameters.PresentationInterval = PresentInterval.Immediate;
            parameters.IsFullScreen = false;

            XnaHelper.GraphicsDevice = new GraphicsDevice(
                GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, parameters);
        }
        #endregion
    }
}
