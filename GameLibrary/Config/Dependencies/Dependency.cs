using System;
using Xemio.GameLibrary.Config.Installation;

namespace Xemio.GameLibrary.Config.Dependencies
{
    internal class Dependency
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Dependency"/> class.
        /// </summary>
        /// <param name="exists">The exists function.</param>
        /// <param name="createInstaller">The create installer.</param>
        public Dependency(Func<bool> exists, Func<IInstaller> createInstaller)
        {
            this.Exists = exists;
            this.CreateInstaller = createInstaller;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the exists function.
        /// </summary>
        public Func<bool> Exists { get; private set; }
        /// <summary>
        /// Gets the installer factory method.
        /// </summary>
        public Func<IInstaller> CreateInstaller { get; private set; } 
        #endregion
    }
}
