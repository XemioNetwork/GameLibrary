using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.UI.DataBindings;
using Xemio.GameLibrary.UI.Events;

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
            this._dataBinder = new DataBinder(this);
        }
        #endregion

        #region Fields
        private readonly IList<Widget> _widgets;
        private readonly DataBinder _dataBinder;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public WidgetState State { get; private set; }
        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public IWidgetContainer Parent { get; internal set; }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the widget gets painted.
        /// </summary>
        public event EventHandler<PaintEventArgs> Paint;
        /// <summary>
        /// Occurs when a mouse button gets pressed.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseDown;
        /// <summary>
        /// Occurs when a mouse button gets released.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseUp;
        /// <summary>
        /// Occurs when the mouse moved.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseMove;
        /// <summary>
        /// Occurs when a key got pressed.
        /// </summary>
        public event EventHandler<KeyEventArgs> KeyDown;
        /// <summary>
        /// Occurs when a key got released.
        /// </summary>
        public event EventHandler<KeyEventArgs> KeyUp;
        /// <summary>
        /// Occurs when the widget got focused.
        /// </summary>
        public event EventHandler GotFocus;
        /// <summary>
        /// Occurs when the widget lost focus.
        /// </summary>
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
        /// Updates the bindings.
        /// </summary>
        public void UpdateBindings()
        {
            this._dataBinder.UpdateBindings();
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
            this.UpdateBindings();
        }
        #endregion

        #region Event Methods
        /// <summary>
        /// Called when the widget is asked to paint.
        /// </summary>
        protected virtual void OnPaint(PaintEventArgs e)
        {
            if (this.Paint != null)
            {
                this.Paint(this, e);
            }
        }
        /// <summary>
        /// Raises the <see cref="E:MouseDown"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Xemio.GameLibrary.UI.Events.MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            if (this.MouseDown != null)
            {
                this.MouseDown(this, e);
            }
        }
        /// <summary>
        /// Raises the <see cref="E:MouseUp"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Xemio.GameLibrary.UI.Events.MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            if (this.MouseUp != null)
            {
                this.MouseUp(this, e);
            }
        }
        /// <summary>
        /// Raises the <see cref="E:MouseMove"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Xemio.GameLibrary.UI.Events.MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            if (this.MouseMove != null)
            {
                this.MouseMove(this, e);
            }
        }
        /// <summary>
        /// Raises the <see cref="E:KeyDown"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Xemio.GameLibrary.UI.Events.KeyEventArgs"/> instance containing the event data.</param>
        protected virtual void OnKeyDown(KeyEventArgs e)
        {
            if (this.KeyDown != null)
            {
                this.KeyDown(this, e);
            }
        }
        /// <summary>
        /// Raises the <see cref="E:KeyUp"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Xemio.GameLibrary.UI.Events.KeyEventArgs"/> instance containing the event data.</param>
        protected virtual void OnKeyUp(KeyEventArgs e)
        {
            if (this.KeyUp != null)
            {
                this.KeyUp(this, e);
            }
        }
        /// <summary>
        /// Called when the widget gets focused.
        /// </summary>
        protected virtual void OnGotFocus()
        {
            if (this.GotFocus != null)
            {
                this.GotFocus(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Called when the widget loses focus.
        /// </summary>
        protected virtual void OnLostFocus()
        {
            if (this.LostFocus != null)
            {
                this.LostFocus(this, EventArgs.Empty);
            }
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