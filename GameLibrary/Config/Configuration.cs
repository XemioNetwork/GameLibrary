using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Config.Installers;

namespace Xemio.GameLibrary.Config
{
    public class Configuration : ICatalog<IInstaller>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            this._installers = new List<IInstaller>();
        }
        #endregion

        #region Fields
        private readonly IList<IInstaller> _installers; 
        #endregion

        #region Properties
        /// <summary>
        /// Gets the installers.
        /// </summary>
        internal IEnumerable<IInstaller> Installers
        {
            get { return this._installers; }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Injects the specified installer.
        /// </summary>
        /// <typeparam name="T">The installer type.</typeparam>
        /// <param name="installer">The installer.</param>
        public void Override<T>(T installer) where T : class, IInstaller
        {
            this.Remove<T>();
            this.Install(installer);
        }
        /// <summary>
        /// Installs the specified installer, if it is not contained inside the current configuration.
        /// </summary>
        /// <typeparam name="T">The installer type.</typeparam>
        public T GetOrInstall<T>() where T : class, IInstaller, new()
        {
            if (!this.Has<T>())
            {
                this.Install(new T());
            }

            return this.Get<T>();
        }
        /// <summary>
        /// Determines whether the specified installer is contained inside this configuration.
        /// </summary>
        /// <typeparam name="T">The installer type.</typeparam>
        public bool Has<T>() where T : class, IInstaller
        {
            return this._installers.Any(installer => installer.Unwrap() is T);
        }
        #endregion

        #region Implementation of ICatalog<IInstaller>
        /// <summary>
        /// Installs the specified installer.
        /// </summary>
        /// <param name="installer">The installer.</param>
        public void Install(IInstaller installer)
        {
            this._installers.Add(installer);
        }
        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="installer">The installer.</param>
        public void Remove(IInstaller installer)
        {
            this._installers.Remove(installer);
        }
        /// <summary>
        /// Removes an item by a specified type.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        public void Remove<TItem>() where TItem : class, IInstaller
        {
            this._installers.Remove(this._installers.FirstOrDefault(installer => installer.Unwrap() is TItem));
        }
        /// <summary>
        /// Gets an item by a specified type.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        public TItem Get<TItem>() where TItem : class, IInstaller
        {
            return (TItem)this._installers.FirstOrDefault(installer => installer.Unwrap() is TItem);
        }
        /// <summary>
        /// Requires this instance.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        TItem ICatalog<IInstaller>.Require<TItem>()
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}
