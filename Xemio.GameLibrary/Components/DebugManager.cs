using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Plugins;

namespace Xemio.GameLibrary.Components
{
    public class DebugManager : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DebugManager"/> class.
        /// </summary>
        public DebugManager()
        {
            this.DebugMode = false;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether the game is debugging.
        /// </summary>
        public bool DebugMode { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Toggles the debug mode.
        /// </summary>
        public void ToggleDebugMode()
        {
            this.DebugMode = !this.DebugMode;
        }
        #endregion
    }
}
