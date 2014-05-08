using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Shapes.Cached
{
    public class CachedLineShape : LineShape
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

        #region Overrides of LineShape
        /// <summary>
        /// Gets the position.
        /// </summary>
        public override Vector2 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                this.NeedsRedraw = true;
            }
        }
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public override Vector2 Direction
        {
            get { return base.Direction; }
            set
            {
                base.Direction = value;
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
        /// Renders the shape.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public override void Render(GraphicsDevice graphicsDevice)
        {
            var region = new Rectangle(this.Position.X, this.Position.Y, this.End.X, this.End.Y);

            if (this.RenderTarget == null || this.NeedsRenderTarget)
            {
                this.NeedsRenderTarget = false;
                this.RenderTarget = graphicsDevice.Factory.CreateTarget((int)region.Width, (int)region.Height);
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

            graphicsDevice.Render(this.RenderTarget, region);
        }
        #endregion
    }
}
