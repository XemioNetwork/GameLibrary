using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Plugins
{
    public class PluginAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginAttribute"/> class.
        /// </summary>
        public PluginAttribute() : this(string.Empty, string.Empty)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="author">The author.</param>
        public PluginAttribute(string name, string author) : this(name, author, 1)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="author">The author.</param>
        public PluginAttribute(string name, string author, int version)
        {
            this.Name = name;
            this.Author = author;
            this.Version = version;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the author.
        /// </summary>
        public string Author { get; private set; }
        /// <summary>
        /// Gets the version.
        /// </summary>
        public int Version { get; private set; }
        #endregion
    }
}
