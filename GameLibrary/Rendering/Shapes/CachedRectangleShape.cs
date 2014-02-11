using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Effects;

namespace Xemio.GameLibrary.Rendering.Shapes
{
    internal class CachedRectangleShape : RectangleShape
    {
        #region Properties
        /// <summary>
        /// Gets the render target.
        /// </summary>
        protected IRenderTarget RenderTarget { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the shape has changed.
        /// </summary>
        protected bool HasChanged { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether the rectangle has changed.
        /// </summary>
        protected bool HasRegionChanged { get; private set; }
        #endregion

        #region Overrides of RectangleShape
        /// <summary>
        /// Gets or sets the background brush.
        /// </summary>
        public override IBrush Background
        {
            get { return base.Background; }
            set
            {
                base.Background = value;
                this.HasChanged = true;
            }
        }
        /// <summary>
        /// Gets or sets the outline pen.
        /// </summary>
        public override IPen Outline
        {
            get { return base.Outline; }
            set
            {
                base.Outline = value;
                this.HasChanged = true;
            }
        }
        /// <summary>
        /// Gets or sets the rectangle.
        /// </summary>
        public override Rectangle Region
        {
            get { return base.Region; }
            set
            {
                base.Region = value;

                this.HasChanged = true;
                this.HasRegionChanged = true;
            }
        }
        /// <summary>
        /// Renders the shape.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        public override void Render(IRenderManager renderManager)
        {
            var graphicsDevice = XGL.Components.Get<GraphicsDevice>();
            if (this.RenderTarget == null || this.HasRegionChanged)
            {
                this.RenderTarget = graphicsDevice.RenderFactory.CreateTarget((int)this.Region.Width, (int)this.Region.Height);
            }

            if (this.HasChanged)
            {
                using (graphicsDevice.RenderTo(this.RenderTarget))
                {
                    //Reset origin to 0,0 causing the RenderManager to
                    //render properly inside the render target.

                    IShape shape = this;
                    using (renderManager.Translate(-shape.Position))
                    {
                        base.Render(renderManager);
                    }
                }
            }

            renderManager.Render(this.RenderTarget, this.Region);
        }
        #endregion
    }
}
