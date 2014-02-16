using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Surfaces;

namespace Xemio.GameLibrary.Rendering.Xna
{
    using XnaColor = Microsoft.Xna.Framework.Color;
    using XnaRectangle = Microsoft.Xna.Framework.Rectangle;
    using XnaVector2 = Microsoft.Xna.Framework.Vector2;
    using XnaGraphicsDevice = Microsoft.Xna.Framework.Graphics.GraphicsDevice;

    internal static class XnaHelper
    {
        #region Properties
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public static XnaGraphicsDevice GraphicsDevice { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Converts the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        public static XnaColor Convert(Color color)
        {
            return new XnaColor(color.R, color.G, color.B, color.A);
        }
        /// <summary>
        /// Converts the specified rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public static XnaRectangle Convert(Rectangle rectangle)
        {
            return new XnaRectangle(
                (int)rectangle.X,
                (int)rectangle.Y,
                (int)rectangle.Width,
                (int)rectangle.Height);
        }
        /// <summary>
        /// Converts the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public static XnaVector2 Convert(Vector2 vector)
        {
            return new XnaVector2(vector.X, vector.Y);
        }
        /// <summary>
        /// Converts the specified interpolation mode.
        /// </summary>
        /// <param name="interpolation">The interpolation.</param>
        public static SamplerState Convert(InterpolationMode interpolation)
        {
            switch (interpolation)
            {
                case InterpolationMode.NearestNeighbor:
                    return SamplerState.PointClamp;
                case InterpolationMode.Linear:
                    return SamplerState.LinearClamp;
                case InterpolationMode.Bicubic:
                    return SamplerState.AnisotropicClamp;
                default:
                    throw new ArgumentOutOfRangeException("interpolation");
            }
        }
        /// <summary>
        /// Tries to instantiate a new graphics device. Returns false if the device creation failed.
        /// </summary>
        public static bool TryCreateDevice()
        {
            try
            {
                Control control = new Control();

                var parameters = new PresentationParameters
                {
                    BackBufferWidth = 400,
                    BackBufferHeight = 300,
                    BackBufferFormat = SurfaceFormat.Color,
                    DepthStencilFormat = DepthFormat.Depth24,
                    DeviceWindowHandle = control.Handle,
                    PresentationInterval = PresentInterval.Immediate,
                    IsFullScreen = false
                };

                //Just call the constructor to see if we can create a XNA graphics device
                new XnaGraphicsDevice(
                    GraphicsAdapter.DefaultAdapter,
                    GraphicsProfile.HiDef,
                    parameters);

                return true;
            }
            catch (Exception ex)
            {
                var eventManager = XGL.Components.Get<EventManager>();
                eventManager.Publish(new ExceptionEvent(ex));

                return false;
            }
        }
        /// <summary>
        /// Creates the device.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public static void CreateDevice(GraphicsDevice graphicsDevice)
        {
            var surface = XGL.Components.Require<WindowSurface>();

            var parameters = new PresentationParameters
            {
                BackBufferWidth = graphicsDevice.DisplayMode.Width,
                BackBufferHeight = graphicsDevice.DisplayMode.Height,
                BackBufferFormat = SurfaceFormat.Color,
                DepthStencilFormat = DepthFormat.Depth24,
                DeviceWindowHandle = surface.Handle,
                PresentationInterval = PresentInterval.Immediate,
                IsFullScreen = false
            };

            XnaHelper.GraphicsDevice = new XnaGraphicsDevice(
                GraphicsAdapter.DefaultAdapter,
                GraphicsProfile.HiDef,
                parameters);
        }
        #endregion
    }
}