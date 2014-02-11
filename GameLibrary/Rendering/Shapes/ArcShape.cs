using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Shapes
{
    public class ArcShape : IShape
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ArcShape"/> class.
        /// </summary>
        internal protected ArcShape()
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
        /// <summary>
        /// Gets or sets the start angle.
        /// </summary>
        public virtual float StartAngle { get; set; }
        /// <summary>
        /// Gets or sets the sweep angle.
        /// </summary>
        public virtual float SweepAngle { get; set; }
        #endregion

        #region Implementation of IShape
        /// <summary>
        /// Gets the position.
        /// </summary>
        Vector2 IShape.Position { get; set; }
        /// <summary>
        /// Gets or sets the background brush.
        /// </summary>
        IBrush IShape.Background
        {
            get { return default(IBrush); }
            set { throw new NotSupportedException("Setting the background of an ArcShape is not supported."); }
        }
        /// <summary>
        /// Gets or sets the outline pen.
        /// </summary>
        public virtual IPen Outline { get; set; }
        /// <summary>
        /// Renders the shape.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        public virtual void Render(IRenderManager renderManager)
        {
            if (this.Outline != null)
            {
                renderManager.DrawArc(this.Outline, this.Region, this.StartAngle, this.SweepAngle);
            }
        }
        #endregion
    }
}
