using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Script
{
    public class ScriptCollection : List<IScript>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptCollection"/> class.
        /// </summary>
        public ScriptCollection()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptCollection"/> class.
        /// </summary>
        /// <param name="scripts">The scripts.</param>
        public ScriptCollection(IEnumerable<IScript> scripts)
        {
            this.AddRange(scripts);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets a script by a specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public IScript GetScript(string name)
        {
            return this.FirstOrDefault(script => script.GetType().Name == name);
        }
        #endregion
    }
}
