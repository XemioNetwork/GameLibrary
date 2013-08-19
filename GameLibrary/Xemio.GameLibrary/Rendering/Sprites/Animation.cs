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
        /// Initializes a new instance of the <see cref="Animation"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="texture">The texture.</param>
        public Animation(string name, ITexture texture)
        {
            this.Name = name;
            this.Indices = new[] {0};
            this.Sheet = new SpriteSheet(texture, texture.Width, texture.Height);
            this.FrameTime = 0;
            this.IsLooped = false;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Animation"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sheet">The sheet.</param>
        /// <param name="frameTime">The frame time.</param>
        public Animation(string name, SpriteSheet sheet, float frameTime)
            : this(name, sheet, frameTime, true)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Animation"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sheet">The sheet.</param>
        /// <param name="frameTime">The frame time.</param>
        /// <param name="isLooped">if set to <c>true</c> the animation gets looped.</param>
        public Animation(string name, SpriteSheet sheet, float frameTime, bool isLooped)
        {
            this.Name = name;
            this.Sheet = sheet;
            this.FrameTime = frameTime;

            this.IsLooped = isLooped;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets or sets the indices.
        /// </summary>
        public int[] Indices { get; set; }
        /// <summary>
        /// Gets the sheet.
        /// </summary>
        public SpriteSheet Sheet { get; private set; }
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
        /// Creates a new animation instance.
        /// </summary>
        public AnimationInstance CreateInstance()
        {
            return new AnimationInstance(this);
        }
        #endregion
    }
}
