using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Network.SyncputServer.Scenes
{
    public class TransitionScene : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TransitionScene"/> class.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="current">The current.</param>
        /// <param name="next">The next.</param>
        public TransitionScene(TransitionDirection direction, ListScene current, ListScene next)
        {
            this.Direction = direction;
            this.Current = current;

            this.Next = next;
            this.SceneManager.Add(next);
        }
        #endregion

        #region Fields
        private Vector2 _cameraOffset;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the multiplier.
        /// </summary>
        protected float Multiplier
        {
            get
            {
                int sign = 1;

                if (this.Direction == TransitionDirection.Left)
                    sign = -1;

                return sign;
            }
        }
        /// <summary>
        /// Gets the direction.
        /// </summary>
        public TransitionDirection Direction { get; private set; }
        /// <summary>
        /// Gets the current scene.
        /// </summary>
        public ListScene Current { get; private set; }
        /// <summary>
        /// Gets the next scene.
        /// </summary>
        public ListScene Next { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this.SceneManager.Remove(this.Current);
        }
        /// <summary>
        /// Ticks the specified elapsed.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            Vector2 screenWidth = new Vector2(this.GraphicsDevice.DisplayMode.Width, 0);
            this._cameraOffset += new Vector2(this.Multiplier, 0) * 10;

            this.Current.RenderOffset = this._cameraOffset;
            this.Next.RenderOffset = this._cameraOffset - screenWidth * this.Multiplier;

            if (this._cameraOffset.X < screenWidth.X * (-1) ||
                this._cameraOffset.X > screenWidth.X)
            {
                this.SceneManager.Remove(this.Current);
                this.Next.RenderOffset = Vector2.Zero;

                this.Remove();
            }
        }
        #endregion
    }
}
