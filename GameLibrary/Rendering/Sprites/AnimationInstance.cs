using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.Sprites
{
    public class AnimationInstance
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationInstance"/> class.
        /// </summary>
        /// <param name="animation">The animation.</param>
        public AnimationInstance(Animation animation)
        {
            this.Animation = animation;
        }
        #endregion

        #region Fields
        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether the animation is finished.
        /// </summary>
        public bool IsFinished
        {
            get { return !this.Animation.IsLooped && this.Index == this.Animation.Indices.Length - 1; }
        }
        /// <summary>
        /// Gets the animation.
        /// </summary>
        public Animation Animation { get; private set; }
        /// <summary>
        /// Gets the index.
        /// </summary>
        public int Index { get; private set; }
        /// <summary>
        /// Gets the index of the sprite.
        /// </summary>
        public int SpriteIndex
        {
            get { return this.Animation.Indices[this.Index]; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Resets this animation.
        /// </summary>
        public void Reset()
        {
            this._elapsed = 0;
            this.Index = 0;
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            this._elapsed += elapsed;
            while (this._elapsed >= this.Animation.FrameTime && 
                   this.Animation.FrameTime > 0 &&
                   this.Index < this.Animation.Indices.Length)
            {
                this._elapsed -= this.Animation.FrameTime;
                this.Index++;

                if (this.Index >= this.Animation.Indices.Length)
                {
                    if (this.Animation.IsLooped)
                        this.Index = 0;
                    else
                        this.Index = this.Animation.Indices.Length - 1;
                }
            }
        }
        #endregion
    }
}
