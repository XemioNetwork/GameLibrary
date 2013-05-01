using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.UI.CSS.Namespaces;
using Xemio.GameLibrary.UI.CSS.Properties;

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
            this.Properties = new List<IStyleProperty>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        public INamespace Namespace { get; private set; }
        /// <summary>
        /// Gets the properties.
        /// </summary>
        public List<IStyleProperty> Properties { get; private set; } 
        #endregion
    }
}
