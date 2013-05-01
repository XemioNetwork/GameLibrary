using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.UI.CSS.Rendering.Text;
using Xemio.GameLibrary.UI.Shapes;
using Xemio.GameLibrary.UI.Widgets;

namespace Xemio.GameLibrary.UI.CSS.Rendering
{
    public class WidgetRenderState
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetRenderState"/> class.
        /// </summary>
        /// <param name="widget">The widget.</param>
        public WidgetRenderState(Widget widget)
        {
            this.Widget = widget;

            this.Shape = new RectangleShape();
            this.ForeColor = Color.Black;
            this.BackColor = Color.Transparent;
            this.HighlightColor = Color.Transparent;
            this.Position = Vector2.Zero;
            this.Padding = new Padding(5, 5, 5, 5);
            this.TextTransform = new DefaultTransform();
            this.Border = BorderStyle.None;
            this.InnerBorder = BorderStyle.None;
            this.BorderRadius = 0;
            this.BoxShadow = BoxShadow.None;
            this.TextShadow = TextShadow.None;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the widget.
        /// </summary>
        public Widget Widget { get; private set; }
        /// <summary>
        /// Gets or sets the shape.
        /// </summary>
        public IShape Shape { get; set; }
        /// <summary>
        /// Gets or sets the text color.
        /// </summary>
        public Color ForeColor { get; set; }
        /// <summary>
        /// Gets or sets the color of the back.
        /// </summary>
        public Color BackColor { get; set; }
        /// <summary>
        /// Gets or sets the color of the highlight.
        /// </summary>
        public Color HighlightColor { get; set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Gets or sets the padding.
        /// </summary>
        public Padding Padding { get; set; }
        /// <summary>
        /// Gets or sets the text transform.
        /// </summary>
        public ITextTransformation TextTransform { get; set; }
        /// <summary>
        /// Gets or sets the border.
        /// </summary>
        public BorderStyle Border { get; set; }
        /// <summary>
        /// Gets or sets the inner border.
        /// </summary>
        public BorderStyle InnerBorder { get; set; }
        /// <summary>
        /// Gets or sets the border radius.
        /// </summary>
        public float BorderRadius { get; set; }
        /// <summary>
        /// Gets or sets the box shadow.
        /// </summary>
        public BoxShadow BoxShadow { get; set; }
        /// <summary>
        /// Gets or sets the text shadow.
        /// </summary>
        public TextShadow TextShadow { get; set; }
        #endregion
    }
}
