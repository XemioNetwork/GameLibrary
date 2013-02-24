using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Game
{
    public abstract class Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        public Scene()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this <see cref="Scene"/> is loaded.
        /// </summary>
        public bool Loaded { get; private set; }
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return ComponentManager.Get<GraphicsDevice>(); }
        }
        /// <summary>
        /// Gets the key listener.
        /// </summary>
        public KeyListener Keyboard
        {
            get { return ComponentManager.Get<KeyListener>(); }
        }
        /// <summary>
        /// Gets the mouse listener.
        /// </summary>
        public MouseListener Mouse
        {
            get { return ComponentManager.Get<MouseListener>(); }
        }
        /// <summary>
        /// Gets the scene manager.
        /// </summary>
        public SceneManager SceneManager
        {
            get { return ComponentManager.Get<SceneManager>(); }
        }
        #endregion

        #region Scene Methods
        /// <summary>
        /// Brings the scene to the front.
        /// </summary>
        public void BringToFront()
        {
            this.SceneManager.Remove(this);
            this.SceneManager.Add(this);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the scene.
        /// </summary>
        public virtual void Initialize()
        {
        }
        /// <summary>
        /// Loads the scene content.
        /// </summary>
        public virtual void LoadContent()
        {
            this.Loaded = true;
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(float elapsed)
        {
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        public virtual void Render()
        {
        }
        #endregion
    }
}
