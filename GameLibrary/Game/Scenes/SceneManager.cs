using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Events.Handles;
using Xemio.GameLibrary.Game.Subscribers;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Game.Scenes
{
    public class SceneManager : SceneContainer, IRenderSubscriber, ITickSubscriber, IConstructable, IHandleContainer
    {
        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether to present the changes to the control.
        /// </summary>
        public bool PresentChanges { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified scenes.
        /// </summary>
        /// <param name="scenes">The scenes.</param>
        public void Add(params Scene[] scenes)
        {
            this.Add((IEnumerable<Scene>)scenes);
        }
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
            this.PresentChanges = true;

            var loop = XGL.Components.Get<IGameLoop>();
            loop.Subscribe(this);
        }
        #endregion

        #region Implementation of IHandleContainer
        /// <summary>
        /// Gets an instance list containing handle implementations.
        /// </summary>
        IEnumerable IHandleContainer.Children
        {
            get { return this.Scenes; }
        }
        #endregion
    }
}
