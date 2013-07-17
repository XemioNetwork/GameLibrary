using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Sprites;
using Xemio.GameLibrary.UI.Widgets.Base;

namespace Xemio.GameLibrary.UI.Widgets
{
    public class ThemedWidget : Widget
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemedWidget"/> class.
        /// </summary>
        /// <param name="spriteSheet">The sprite sheet.</param>
        public ThemedWidget(SpriteSheet spriteSheet)
        {
            this.SpriteSheet = spriteSheet;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sprite sheet.
        /// </summary>
        public SpriteSheet SpriteSheet { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Called when the widget is asked to paint.
        /// </summary>
        /// <param name="e">An instance containing event data.</param>
        protected override void OnPaint(Events.PaintEventArgs e)
        {
            float width = this.Bounds.Width;
            float height = this.Bounds.Height;

            int frameWidth = this.SpriteSheet.FrameWidth;
            int frameHeight = this.SpriteSheet.FrameHeight;

            ThemedSpriteSheet sheet = new ThemedSpriteSheet(this.SpriteSheet);
            
            e.Graphics.Render(sheet.TopLeft, new Vector2(0, 0));
            e.Graphics.Render(sheet.Top, new Rectangle(frameWidth, 0, width - frameWidth * 2, frameHeight));
            e.Graphics.Render(sheet.TopRight, new Vector2(width - frameWidth, 0));

            e.Graphics.Render(sheet.Left, new Rectangle(0, frameHeight, frameWidth, height - frameHeight * 2));
            e.Graphics.Render(sheet.Center, new Rectangle(frameWidth, frameHeight, width - frameWidth * 2, height - frameHeight * 2));
            e.Graphics.Render(sheet.Right, new Rectangle(width - frameWidth, frameHeight, frameWidth, height - frameHeight * 2));

            e.Graphics.Render(sheet.BottomLeft, new Vector2(0, height - frameHeight));
            e.Graphics.Render(sheet.Bottom, new Rectangle(frameWidth, height - frameHeight, width - frameWidth * 2, frameHeight));
            e.Graphics.Render(sheet.BottomRight, new Vector2(width - frameWidth, height - frameHeight));

            base.OnPaint(e);
        }
        #endregion
    }
}
