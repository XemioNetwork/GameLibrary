using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Game
{
    public class CachedSceneProvider : ISceneProvider
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CachedSceneProvider"/> class.
        /// </summary>
        public CachedSceneProvider()
        {
            this._addCache = new Queue<Scene>();
            this._removeCache = new Queue<Scene>();
        }
        #endregion

        #region Fields
        private bool _enumerating;

        private Queue<Scene> _addCache;
        private Queue<Scene> _removeCache;
        #endregion

        #region Methods
        /// <summary>
        /// Called when a scene gets added.
        /// </summary>
        /// <param name="scene">The scene.</param>
        protected virtual void OnAddScene(Scene scene)
        {
        }
        /// <summary>
        /// Called when a scene gets removed.
        /// </summary>
        /// <param name="scene">The scene.</param>
        protected virtual void OnRemoveScene(Scene scene)
        {
        }
        /// <summary>
        /// Applies the cached changes made to the scene list.
        /// </summary>
        public void ApplyCachedChanges()
        {
            foreach (Scene scene in this._addCache) this.Add(scene);
            foreach (Scene scene in this._removeCache) this.Remove(scene);

            this._addCache.Clear();
            this._removeCache.Clear();
        }
        /// <summary>
        /// Begins the enumeration.
        /// </summary>
        internal void BeginEnumeration()
        {
            this._enumerating = true;
        }
        /// <summary>
        /// Ends the enumeration.
        /// </summary>
        internal void EndEnumeration()
        {
            this._enumerating = false;
            this.ApplyCachedChanges();
        }
        #endregion

        #region ISceneProvider Member
        /// <summary>
        /// Adds the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        public void Add(Scene scene)
        {
            if (this._enumerating)
            {
                this._addCache.Enqueue(scene);
            }
            else
            {
                scene.Parent = this;
                scene.Initialize();

                this.OnAddScene(scene);
            }
        }
        /// <summary>
        /// Removes the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        public void Remove(Scene scene)
        {
            if (this._enumerating)
            {
                this._removeCache.Enqueue(scene);
            }
            else
            {
                scene.Parent = null;
                this.OnRemoveScene(scene);
            }
        }
        #endregion
    }
}
