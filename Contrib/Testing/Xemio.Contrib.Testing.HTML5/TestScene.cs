using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Math;

namespace Xemio.Contrib.Testing.HTML5
{
    public class TestScene : Scene
    {
        #region Fields
        private Vector2 _position;
        #endregion

        #region Methods
        public override void Tick(float elapsed)
        {
            this._position += new Vector2(1, 1);
            base.Tick(elapsed);
        }
        public override void Render()
        {
            this.GeometryManager.FillRectangle(null, new Rectangle(0, 0, 20, 20) + this._position);
            base.Render();
        }
        #endregion
    }
}
