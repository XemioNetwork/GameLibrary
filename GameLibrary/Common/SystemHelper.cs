using System;

namespace Xemio.GameLibrary.Common
{
    public class SystemHelper
    {
        #region Properties
        /// <summary>
        /// Gets a value indicating whether the game library is currently running on mac.
        /// </summary>
        public static bool IsMac
        {
            get { return Environment.OSVersion.Platform == PlatformID.MacOSX; }
        }
        /// <summary>
        /// Gets a value indicating whether the game library is currently running on linux.
        /// </summary>
        public static bool IsLinux
        {
            get { return Environment.OSVersion.Platform == PlatformID.Unix; }
        }
        /// <summary>
        /// Gets a value indicating whether the game library is currently running on windows.
        /// </summary>
        public static bool IsWindows
        {
            get { return Environment.OSVersion.Platform == PlatformID.Win32NT ||
                         Environment.OSVersion.Platform == PlatformID.Win32S ||
                         Environment.OSVersion.Platform == PlatformID.Win32Windows ||
                         Environment.OSVersion.Platform == PlatformID.WinCE; }
        }
        #endregion
    }
}

