using System;

namespace Xemio.GameLibrary.Config.Dependencies
{
    public class DependencyException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyException"/> class.
        /// </summary>
        /// <param name="installerType">Type of the installer.</param>
        public DependencyException(Type installerType) : base(string.Format("Installer [{0}] was required by another installer." +
            "Make sure to provide the specified installer inside the configuration.", installerType))
        {
            this.InstallerType = installerType;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the type of the installer.
        /// </summary>
        public Type InstallerType { get; private set; }
        #endregion
    }
}
