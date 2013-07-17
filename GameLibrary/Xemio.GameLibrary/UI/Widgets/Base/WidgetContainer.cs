using System.Collections.Generic;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.UI.Widgets.View;
using Xemio.GameLibrary.UI.Widgets.View.Instructions;

namespace Xemio.GameLibrary.UI.Widgets.Base
{
    public class WidgetContainer : IWidgetContainer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Widget"/> class.
        /// </summary>
        public WidgetContainer()
        {
            this._widgets = new List<Widget>();
        }
        #endregion

        #region Fields
        private readonly IList<Widget> _widgets;
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            for (int i = this._widgets.Count - 1; i >= 0; i--)
            {
                this._widgets[i].Tick(elapsed);
            }
        }
        /// <summary>
        /// Renders the specified widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
        private void Render(Widget widget)
        {
            WidgetView view = widget.View;
            WidgetGraphics graphics = new WidgetGraphics(widget);

            if (view != null)
            {

                foreach (IWidgetViewInstruction item in view.Current.Instructions)
                {
                    Rectangle bounds = item.Shape.Bounds;

                    item.Shape.Bounds = new Rectangle(
                        bounds.X*widget.Bounds.Width,
                        bounds.Y*widget.Bounds.Height,
                        bounds.Width*widget.Bounds.Width,
                        bounds.Height*widget.Bounds.Height);

                    item.Render(graphics);
                    item.Shape.Bounds = bounds;
                }
            }

            widget.Render();
            foreach (Widget child in widget.Widgets)
            {
                this.Render(child);
            }
        }
        /// <summary>
        /// Renders all widget instances.
        /// </summary>
        public void Render()
        {
            for (int i = 0; i < this._widgets.Count; i++)
            {
                this.Render(this._widgets[i]);
            }
        }
        #endregion

        #region Implementation of IWidgetContainer
        /// <summary>
        /// Adds the specified widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
        public void Add(Widget widget)
        {
            widget.Parent = this;
            this._widgets.Add(widget);
        }
        /// <summary>
        /// Removes the specified widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
        public void Remove(Widget widget)
        {
            widget.Parent = null;
            this._widgets.Remove(widget);
        }
        /// <summary>
        /// Gets the widgets.
        /// </summary>
        public IEnumerable<Widget> Widgets
        {
            get { return this._widgets; }
        }
        #endregion

        #region IWidgetContainer Member
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Gets the absolute position.
        /// </summary>
        public Vector2 AbsolutePosition
        {
            get { return this.Position; }
        }
        #endregion
    }
}
