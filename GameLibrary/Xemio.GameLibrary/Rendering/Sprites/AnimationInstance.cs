using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering.Textures;

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
        private int _frameIndex;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the animation.
        /// </summary>
        public Animation Animation { get; private set; }
        /// <summary>
        /// Gets the index of the sprite.
        /// </summary>
        public int SpriteIndex
        {
            get { return this.Animation.Indices[this._frameIndex]; }
        }
        /// <summary>
        /// Gets the current frame.
        /// </summary>
        public ITexture Frame
        {
            get { return this.Animation.Sheet.GetTexture(this.SpriteIndex); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Resets this animation.
        /// </summary>
        public void Reset()
        {
            this._elapsed = 0;
            this._frameIndex = 0;
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
                   this._frameIndex < this.Animation.Indices.Length)
            {
                this._elapsed -= this.Animation.FrameTime;
                this._frameIndex++;

                if (this._frameIndex >= this.Animation.Indices.Length)
                {
                    if (this.Animation.IsLooped)
                    {
                        this._frameIndex = 0;
                    }
                    else
                    {
                        this._frameIndex = this.Animation.Indices.Length - 1;
                    }
                }
            }
        }
        #endregion
    }
}
