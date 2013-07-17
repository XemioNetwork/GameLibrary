using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.UI.Shapes;
using Xemio.GameLibrary.UI.Widgets.Base;
using Xemio.GameLibrary.UI.Widgets.View.Instructions;

namespace Xemio.GameLibrary.UI.Widgets.View
{
    public class WidgetViewState
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetViewState"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        public WidgetViewState(WidgetState state)
        {
            this.State = state;
            this.Visible = true;

            this.Instructions = new List<IWidgetViewInstruction>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the state.
        /// </summary>
        public WidgetState State { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WidgetViewState"/> is visible.
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// Gets the instructions.
        /// </summary>
        public List<IWidgetViewInstruction> Instructions { get; private set; } 
        #endregion

        #region Methods
        /// <summary>
        /// Draws the specified shape.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="pen">The pen.</param>
        public void Draw(IShape shape, IPen pen)
        {
            this.Instructions.Add(new DrawShapeInstruction(shape, pen));
        }
        /// <summary>
        /// Fills the specified shape.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="brush">The brush.</param>
        public void Fill(IShape shape, IBrush brush)
        {
            this.Instructions.Add(new FillShapeInstruction(shape, brush));
        }
        #endregion
    }
}
