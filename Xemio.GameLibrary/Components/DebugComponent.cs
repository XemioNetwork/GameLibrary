using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Plugins;

namespace Xemio.GameLibrary.Components
{
    [Plugin]
    public class DebugComponent : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DebugComponent"/> class.
        /// </summary>
        public DebugComponent()
        {
            this.Debug = false;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether the game is debugging.
        /// </summary>
        public bool Debug { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Toggles this instance.
        /// </summary>
        public void Toggle()
        {
            this.Debug = !this.Debug;
        }
        #endregion
    }
}
