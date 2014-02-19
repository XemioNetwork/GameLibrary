using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Shapes.Cached
{
    public class CachedPolygonShape : PolygonShape
    {
        #region Fields
        private float _x;
        private float _y;

        private Rectangle _region;
        #endregion

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

        #region Overrides of PolygonShape
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
        /// Gets or sets the vertices.
        /// </summary>
        public override Vector2[] Vertices
        {
            get { return base.Vertices; }
            set
            {
                base.Vertices = value;

                this.NeedsRedraw = true;
                this.NeedsRenderTarget = true;

                this._x = value.Min(v => v.X);
                this._y = value.Min(v => v.Y);

                float x = this._x > 0 ? 0 : MathHelper.Abs(this._x);
                float y = this._y > 0 ? 0 : MathHelper.Abs(this._y);

                float width = value.Max(v => v.X);
                float height = value.Max(v => v.Y);

                this._region = new Rectangle(0, 0, width + x, height + y);
            }
        }
        /// <summary>
        /// Gets or sets the background brush.
        /// </summary>
        public override IBrush Background
        {
            get { return base.Background; }
            set
            {
                base.Background = value;
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
        /// <param name="renderManager">The render manager.</param>
        public override void Render(IRenderManager renderManager)
        {
            var graphicsDevice = XGL.Components.Get<GraphicsDevice>();
            if (this.RenderTarget == null || this.NeedsRenderTarget)
            {
                this.NeedsRenderTarget = false;
                this.RenderTarget = graphicsDevice.RenderFactory.CreateTarget((int)this._region.Width, (int)this._region.Height);
            }

            if (this.NeedsRedraw)
            {
                this.NeedsRedraw = false;

                using (renderManager.TranslateTo(new Vector2(this._x, this._y)))
                using (graphicsDevice.RenderTo(this.RenderTarget))
                {
                    base.Render(renderManager);
                }
            }

            using (renderManager.Translate(new Vector2(-this._x, -this._y)))
            {
                renderManager.Render(this.RenderTarget, this._region);
            }
        }
        #endregion
    }
}
