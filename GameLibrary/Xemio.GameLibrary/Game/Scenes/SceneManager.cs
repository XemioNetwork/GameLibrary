using System;
using System.Collections.Generic;
using System.Linq;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Game.Scenes
{
    public class SceneManager : SceneProvider, IGameHandler, IConstructable
    {
        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether to present the changes to the control.
        /// </summary>
        public bool PresentChanges { get; set; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Adds the specified scenes.
        /// </summary>
        /// <param name="scenes">The scenes.</param>
        public void Add(IEnumerable<Scene> scenes)
        {
            foreach (Scene scene in scenes)
            {
                base.Add(scene);
            }
        }
        /// <summary>
        /// Adds the specified scenes.
        /// </summary>
        /// <param name="scenes">The scenes.</param>
        public void Add(params Scene[] scenes)
        {
            this.Add((IEnumerable<Scene>)scenes);
        }
        #endregion
        
        #region IGameHandler Member
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        public override void Render()
        {
            base.Render();

            if (this.PresentChanges && this.GraphicsDevice != null)
                this.GraphicsDevice.Present();
        }
        #endregion

        #region IConstructable Member
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            GameLoop loop = XGL.Components.Get<GameLoop>();
            loop.Subscribe(this);

            this.PresentChanges = true;
        }
        #endregion
    }
}
