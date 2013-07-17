using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Input.Events;
using Xemio.GameLibrary.Input.Events.Keyboard;
using Xemio.GameLibrary.Input.Events.Mouse;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.UI.DataBindings;
using Xemio.GameLibrary.UI.Events;
using Xemio.GameLibrary.UI.Widgets.View;

namespace Xemio.GameLibrary.UI.Widgets.Base
{
    public abstract class Widget : IWidgetContainer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Widget"/> class.
        /// </summary>
        protected Widget()
        {
            this._widgets = new List<Widget>();
            this._dataBinder = new DataBinder(this);

            this.State = WidgetState.Normal;

            EventManager eventManager = XGL.GetComponent<EventManager>();
            eventManager.Subscribe<IInputEvent>(this.HandleInput);
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
        /// Gets or sets the bounds.
        /// </summary>
        public Rectangle Bounds { get; set; }
        /// <summary>
        /// Gets the absolute bounds.
        /// </summary>
        public Rectangle AbsoluteBounds
        {
            get 
            { 
                Vector2 absolutePosition = this.AbsolutePosition;
                Rectangle absoluteBounds = new Rectangle(
                    absolutePosition.X, 
                    absolutePosition.Y, 
                    this.Bounds.Width,
                    this.Bounds.Height);

                return absoluteBounds;
            }
        }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public WidgetState State { get; private set; }
        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public IWidgetContainer Parent { get; internal set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Widget"/> is focused.
        /// </summary>
        public bool Focused { get; internal set; }
        /// <summary>
        /// Gets or sets the top widget.
        /// </summary>
        public Widget Top { get; set; }
        /// <summary>
        /// Gets or sets the right widget.
        /// </summary>
        public Widget Right { get; set; }
        /// <summary>
        /// Gets or sets the bottom widget.
        /// </summary>
        public Widget Bottom { get; set; }
        /// <summary>
        /// Gets or sets the left widget.
        /// </summary>
        public Widget Left { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Gets the root container.
        /// </summary>
        public IWidgetContainer Root
        {
            get
            {
                IWidgetContainer root = this;
                while (root is Widget)
                {
                    root = (root as Widget).Parent;
                }

                return root;
            }
        }
        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        public WidgetView View { get; protected set; }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user clicks inside the widget.
        /// </summary>
        public event EventHandler<MouseEventArgs> Click;
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
        /// Occurs when the mouse enters the widget bounds.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseEnter;
        /// <summary>
        /// Occurs when the mouse leaves the widget bounds.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseLeave;
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
        /// Handles the user input.
        /// </summary>
        /// <param name="e">The event.</param>
        protected virtual void HandleInput(IInputEvent e)
        {
            KeyEvent keyEvent = e as KeyEvent;
            MouseEvent mouseEvent = e as MouseEvent;
                
            bool inBounds = false;
            if (mouseEvent != null)
            {
                inBounds = this.AbsoluteBounds.Contains(mouseEvent.Position);
            }

            if (e is KeyDownEvent && this.Focused)
                this.OnKeyDown(new KeyEventArgs(keyEvent.Key));

            if (e is KeyUpEvent && this.Focused)
                this.OnKeyUp(new KeyEventArgs(keyEvent.Key));

            if (e is MouseDownEvent && inBounds)
                this.OnMouseDown(new MouseEventArgs(mouseEvent.Position, mouseEvent.Button));

            if (e is MouseUpEvent && this.Focused)
                this.OnMouseUp(new MouseEventArgs(mouseEvent.Position, mouseEvent.Button));
            
            if (e is MouseMoveEvent)
            {
                if (inBounds)
                {
                    this.OnMouseMove(new MouseEventArgs(mouseEvent.Position, mouseEvent.Button));
                }
                if (this.State != WidgetState.Hover && inBounds)
                {
                    this.OnMouseEnter(new MouseEventArgs(mouseEvent.Position, mouseEvent.Button));
                }
                if (this.State == WidgetState.Hover && !inBounds)
                {
                    this.OnMouseLeave(new MouseEventArgs(mouseEvent.Position, mouseEvent.Button));
                }
            }
            
        }
        /// <summary>
        /// Creates a new graphics object.
        /// </summary>
        public virtual IGraphics CreateGraphics()
        {
            return new WidgetGraphics(this);
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(float elapsed)
        {
            this.UpdateBindings();
        }
        /// <summary>
        /// Paints this widget.
        /// </summary>
        internal void Render()
        {
            this.OnPaint(new PaintEventArgs(this));
        }
        /// <summary>
        /// Loads the content.
        /// </summary>
        public virtual void LoadContent()
        {
        }
        /// <summary>
        /// Focuses this widget.
        /// </summary>
        public void Focus()
        {
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
        /// Raises the <see cref="E:Click"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Xemio.GameLibrary.UI.Events.MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnClick(MouseEventArgs e)
        {
            this.Focus();
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }
        /// <summary>
        /// Raises the <see cref="E:MouseDown"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Xemio.GameLibrary.UI.Events.MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            this.State = WidgetState.Pressed;
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
            this.State = WidgetState.Hover;

            if (this.Bounds.Contains(e.Position))
            {
                this.OnClick(e);
            }
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
        /// Raises the <see cref="E:MouseEnter"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Xemio.GameLibrary.UI.Events.MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnMouseEnter(MouseEventArgs e)
        {
            this.State = WidgetState.Hover;
            if (this.MouseEnter != null)
            {
                this.MouseEnter(this, e);
            }
        }
        /// <summary>
        /// Raises the <see cref="E:MouseLeave"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Xemio.GameLibrary.UI.Events.MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnMouseLeave(MouseEventArgs e)
        {
            this.State = WidgetState.Normal;
            if (this.MouseLeave != null)
            {
                this.MouseLeave(this, e);
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
        public Vector2 Position
        {
            get { return new Vector2(this.Bounds.X, this.Bounds.Y); }
            set 
            {
                this.Bounds = new Rectangle(
                    value.X, value.Y, this.Bounds.Width, this.Bounds.Height);
            }
        }
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
        /// <summary>
        /// Gets the widgets.
        /// </summary>
        public IEnumerable<Widget> Widgets
        {
            get { return this._widgets; }
        }
        #endregion
    }
}