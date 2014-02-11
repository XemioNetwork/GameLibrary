using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.Sprites
{
    public class Animation
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Animation" /> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        public Animation(ITexture texture) : this(new SpriteSheet(texture, texture.Width, texture.Height), 0, new[] {0}, false)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Animation" /> class.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <param name="frameTime">The frame time.</param>
        /// <param name="indices">The indices.</param>
        public Animation(SpriteSheet sheet, float frameTime, int[] indices) : this(sheet, frameTime, indices, true)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Animation" /> class.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <param name="frameTime">The frame time.</param>
        /// <param name="indices">The indices.</param>
        /// <param name="isLooped">if set to <c>true</c> the animation gets looped.</param>
        public Animation(SpriteSheet sheet, float frameTime, int[] indices, bool isLooped)
        {
            this.Sheet = sheet;
            this.FrameTime = frameTime;
            this.IsLooped = isLooped;
            this.Indices = indices;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the sprite sheet.
        /// </summary>
        public SpriteSheet Sheet { get; private set; }
        /// <summary>
        /// Gets the frame time.
        /// </summary>
        public float FrameTime { get; private set; }
        /// <summary>
        /// Gets or sets the indices.
        /// </summary>
        public int[] Indices { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is looped.
        /// </summary>
        public bool IsLooped { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new animation instance.
        /// </summary>
        public AnimationInstance CreateInstance()
        {
            return new AnimationInstance(this);
        }
        #endregion
    }
}
