using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.Sprites
{
    public class SpriteAnimation
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteAnimation"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sheet">The sheet.</param>
        /// <param name="frameTime">The frame time.</param>
        public SpriteAnimation(string name, SpriteSheet sheet, float frameTime)
            : this(name, sheet, frameTime, true)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteAnimation"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sheet">The sheet.</param>
        /// <param name="frameTime">The frame time.</param>
        /// <param name="isLooped">if set to <c>true</c> the animation gets looped.</param>
        public SpriteAnimation(string name, SpriteSheet sheet, float frameTime, bool isLooped)
        {
            this.Name = name;
            this.Sheet = sheet;
            this.FrameTime = frameTime;

            this.IsLooped = isLooped;
        }
        #endregion

        #region Fields
        private float _elapsed;
        private int _frameIndex;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the sheet.
        /// </summary>
        public SpriteSheet Sheet { get; private set; }
        /// <summary>
        /// Gets the current frame.
        /// </summary>
        public ITexture CurrentFrame
        {
            get { return this.Sheet.Textures[this._frameIndex]; }
        }
        /// <summary>
        /// Gets the frame time.
        /// </summary>
        public float FrameTime { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is looped.
        /// </summary>
        public bool IsLooped { get; set; }
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
            while (this._elapsed >= this.FrameTime && this.FrameTime > 0)
            {
                this._elapsed -= this.FrameTime;
                this._frameIndex++;

                if (this._frameIndex >= this.Sheet.Textures.Length)
                {
                    if (this.IsLooped)
                    {
                        this._frameIndex = 0;
                    }
                    else
                    {
                        this._frameIndex = this.Sheet.Textures.Length - 1;
                    }
                }
            }
        }
        #endregion
    }
}
