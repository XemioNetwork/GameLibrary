using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SlimDX;
using SlimDX.DXGI;
using SlimDX.Direct3D11;
using Device = SlimDX.Direct3D11.Device;
using Resource = SlimDX.Direct3D11.Resource;
using System.Windows.Forms;

namespace Xemio.GameLibrary.Rendering.SlimDX
{
    public static class DXHelper
    {        
        #region Properties
        /// <summary>
        /// Gets the device.
        /// </summary>
        public static Device GraphicsDevice { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the specified handle.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public static void Initialize(IntPtr handle)
        {
            Control surface = Control.FromHandle(handle);

            SwapChainDescription description = new SwapChainDescription()
            {
                BufferCount = 1,
                Usage = Usage.RenderTargetOutput,
                OutputHandle = handle,
                IsWindowed = true,
                ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                SampleDescription = new SampleDescription(1, 0),
                Flags = SwapChainFlags.AllowModeSwitch,
                SwapEffect = SwapEffect.Discard
            }; 

            Device device;
            SwapChain swapChain;

            Device.CreateWithSwapChain(
                DriverType.Warp, 
                DeviceCreationFlags.None, 
                description, 
                out device,
                out swapChain);

            RenderTargetView renderTarget;
            using (Resource resource = Resource.FromSwapChain<Texture2D>(swapChain, 0))
                renderTarget = new RenderTargetView(device, resource);

            DeviceContext context = device.ImmediateContext;
            Viewport viewport = new Viewport(
                0.0f, 0.0f,
                surface.ClientSize.Width, 
                surface.ClientSize.Height);

            context.OutputMerger.SetTargets(renderTarget);
            context.Rasterizer.SetViewports(viewport);

            DXHelper.GraphicsDevice = device;
        }
        #endregion
    }
}
