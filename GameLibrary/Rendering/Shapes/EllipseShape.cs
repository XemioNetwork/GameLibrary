using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Shapes
{
    public class EllipseShape : IShape
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EllipseShape"/> class.
        /// </summary>
        internal protected EllipseShape()
        {
        }
        #endregion

        #region Fields
        private Rectangle _region;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        public virtual Rectangle Region
        {
            get { return this._region; }
            set
            {
                IShape shape = this;
                shape.Position = new Vector2(value.X, value.Y);

                this._region = value;
            }
        }
        #endregion

        #region Implementation of IShape
        /// <summary>
        /// Gets the position.
        /// </summary>
        Vector2 IShape.Position { get; set; }
        /// <summary>
        /// Gets or sets the outline pen.
        /// </summary>
        public virtual IPen Outline { get; set; }
        /// <summary>
        /// Gets or sets the background brush.
        /// </summary>
        public virtual IBrush Background { get; set; }
        /// <summary>
        /// Renders the shape.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        public virtual void Render(IRenderManager renderManager)
        {
            if (this.Background != null)
            {
                renderManager.FillEllipse(this.Background, this.Region);
            }
            if (this.Outline != null)
            {
                renderManager.DrawEllipse(this.Outline, this.Region);
            }
        }
        #endregion
    }
}
