using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Shapes.Cached
{
    public class CachedArcShape : ArcShape
    {
        #region Properties
        /// <summary>
        /// Gets the render target.
        /// </summary>
        protected IRenderTarget RenderTarget { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the shape has changed.
        /// </summary>
        protected bool NeedsRedraw { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether the rectangle has changed.
        /// </summary>
        protected bool NeedsRenderTarget { get; private set; }
        #endregion

        #region Overrides of ArcShape
        /// <summary>
        /// Gets or sets the start angle.
        /// </summary>
        public override float StartAngle
        {
            get { return base.StartAngle; }
            set
            {
                base.StartAngle = value;
                this.NeedsRedraw = true;
            }
        }
        /// <summary>
        /// Gets or sets the sweep angle.
        /// </summary>
        public override float SweepAngle
        {
            get { return base.SweepAngle; }
            set
            {
                base.SweepAngle = value;
                this.NeedsRedraw = true;
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
                this.NeedsRedraw = true;
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

                this.NeedsRedraw = true;
                this.NeedsRenderTarget = true;
            }
        }
        /// <summary>
        /// Renders the shape.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public override void Render(GraphicsDevice graphicsDevice)
        {
            if (this.RenderTarget == null || this.NeedsRenderTarget)
            {
                this.NeedsRenderTarget = false;
                this.RenderTarget = graphicsDevice.Factory.CreateTarget((int)this.Region.Width, (int)this.Region.Height);
            }

            if (this.NeedsRedraw)
            {
                this.NeedsRedraw = false;

                using (graphicsDevice.TranslateTo(Vector2.Zero))
                using (graphicsDevice.RenderTo(this.RenderTarget))
                {
                    base.Render(graphicsDevice);
                }
            }

            graphicsDevice.Render(this.RenderTarget, this.Region);
        }
        #endregion
    }
}
