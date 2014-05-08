using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Shapes
{
    public class PieShape : IShape
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PieShape"/> class.
        /// </summary>
        internal protected PieShape()
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
        /// <param name="graphicsDevice">The graphics device.</param>
        public virtual void Render(GraphicsDevice graphicsDevice)
        {
            if (this.Background != null)
            {
                graphicsDevice.FillPie(this.Background, this.Region, this.StartAngle, this.SweepAngle);
            }
            if (this.Outline != null)
            {
                graphicsDevice.DrawPie(this.Outline, this.Region, this.StartAngle, this.SweepAngle);
            }
        }
        #endregion
    }
}
