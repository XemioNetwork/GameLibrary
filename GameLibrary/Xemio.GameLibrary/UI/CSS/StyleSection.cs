using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.UI.CSS.Namespaces;

namespace Xemio.GameLibrary.UI.CSS
{
    public class StyleSection
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleSection"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public StyleSection(string name)
        {
            this.Namespace = new Namespace(name);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        public INamespace Namespace { get; private set; }
        #endregion
    }
}
