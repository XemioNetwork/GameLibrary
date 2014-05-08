using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Shapes
{
    public class RectangleShape : IShape
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleShape"/> class.
        /// </summary>
        internal protected RectangleShape()
        {
        }
        #endregion

        #region Fields
        private Rectangle _region;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        public virtual float CornerRadius { get; set; }
        /// <summary>
        /// Gets or sets the rectangle.
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
        /// <param name="graphicsDevice">The graphics device.</param>
        public virtual void Render(GraphicsDevice graphicsDevice)
        {
            if (this.Background != null)
            {
                graphicsDevice.FillRectangle(this.Background, this.Region, this.CornerRadius);
            }
            if (this.Outline != null)
            {
                graphicsDevice.DrawRectangle(this.Outline, this.Region, this.CornerRadius);
            }
        }
        #endregion
    }
}
