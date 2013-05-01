using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.UI.Events;
using Keys = Xemio.GameLibrary.Input.Keys;
using MouseButtons = Xemio.GameLibrary.Input.MouseButtons;

namespace Xemio.GameLibrary.UI.Widgets
{
    public class Widget : IWidgetContainer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Widget"/> class.
        /// </summary>
        public Widget()
        {
            this._widgets = new List<Widget>();
            this._dataBinder = new WidgetDataBinder(this);
        }
        #endregion

        #region Fields
        private readonly IList<Widget> _widgets;
        private readonly WidgetDataBinder _dataBinder;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public IWidgetContainer Parent { get; internal set; }
        #endregion

        #region Events
        public event EventHandler<PaintEventArgs> Paint;
        public event EventHandler<MouseEventArgs> MouseDown;
        public event EventHandler<MouseEventArgs> MouseUp;
        public event EventHandler<MouseEventArgs> MouseMove;
        public event EventHandler<KeyEventArgs> KeyDown;
        public event EventHandler<KeyEventArgs> KeyUp;
        public event EventHandler GotFocus;
        public event EventHandler LostFocus;
        #endregion

        #region Methods
        /// <summary>
        /// Binds the specified property to a model.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="destination">The destination.</param>
        public void Bind<T>(Expression<Func<T>> property, Expression<Func<T>> destination)
        {
            this._dataBinder.Bind(this, property, destination);
        }
        /// <summary>
        /// Loads the content.
        /// </summary>
        public virtual void LoadContent()
        {
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(float elapsed)
        {
        }
        #endregion

        #region Event Methods
        /// <summary>
        /// Called when the widget is asked to paint.
        /// </summary>
        protected virtual void OnPaint(PaintEventArgs e)
        {
        }
        /// <summary>
        /// Raises the <see cref="E:MouseDown"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Xemio.GameLibrary.UI.Events.MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnMouseDown(MouseEventArgs e)
        {
        }
        /// <summary>
        /// Raises the <see cref="E:MouseUp"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Xemio.GameLibrary.UI.Events.MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnMouseUp(MouseEventArgs e)
        {
        }
        /// <summary>
        /// Raises the <see cref="E:MouseMove"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Xemio.GameLibrary.UI.Events.MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnMouseMove(MouseEventArgs e)
        {
        }
        /// <summary>
        /// Raises the <see cref="E:KeyDown"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Xemio.GameLibrary.UI.Events.KeyEventArgs"/> instance containing the event data.</param>
        protected virtual void OnKeyDown(KeyEventArgs e)
        {
        }
        /// <summary>
        /// Raises the <see cref="E:KeyUp"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Xemio.GameLibrary.UI.Events.KeyEventArgs"/> instance containing the event data.</param>
        protected virtual void OnKeyUp(KeyEventArgs e)
        {
        }
        /// <summary>
        /// Called when the widget gets focused.
        /// </summary>
        protected virtual void OnGotFocus()
        {
        }
        /// <summary>
        /// Called when the widget loses focus.
        /// </summary>
        protected virtual void OnLostFocus()
        {
        }
        #endregion

        #region Implementation of IWidgetContainer
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Gets the absolute position.
        /// </summary>
        public Vector2 AbsolutePosition
        {
            get
            {
                if (this.Parent != null)
                {
                    return this.Parent.AbsolutePosition + this.Position;
                }

                return this.Position;
            }
        }
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
        #endregion

        #region Implementation of IEnumerable
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<Widget> GetEnumerator()
        {
            return this._widgets.GetEnumerator();
        }
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}