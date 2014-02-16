using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Rendering;

namespace Xemio.Contrib.Testing.Xna
{
    public class TestScene2 : Scene
    {
        #region Constructors
        public TestScene2()
        {

        }
        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods
        public override void Render()
        {
            this.GraphicsDevice.Clear(Color.Red);
        }
        #endregion
    }
}
