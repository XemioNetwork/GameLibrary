using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Xemio.GameLibrary.Plugins
{
    public class PluginLoader<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginLoader&lt;T&gt;"/> class.
        /// </summary>
        public PluginLoader()
        {
            this.Plugins = new List<T>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the plugins.
        /// </summary>
        public List<T> Plugins { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is recursive.
        /// </summary>
        public bool IsRecursive { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the plugins.
        /// </summary>
        public void LoadPlugins()
        {
            this.LoadPlugins(".");
        }
        /// <summary>
        /// Loads the plugins.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public void LoadPlugins(string directory)
        {
            foreach (string fileName in Directory.GetFiles(directory, "*.dll"))
            {
                this.LoadAssembly(fileName);
            }
        }
        /// <summary>
        /// Loads the specified assembly.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void LoadAssembly(string fileName)
        {
            Assembly assembly = Assembly.LoadFrom(fileName);
            this.LoadAssembly(assembly);
        }
        /// <summary>
        /// Loads the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public void LoadAssembly(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(T).IsAssignableFrom(type))
                {
                    if (!type.IsInterface && !type.IsAbstract && !type.IsGenericType)
                    {
                        this.Plugins.Add((T)Activator.CreateInstance(type));
                    }
                }
            }
        }
        #endregion
    }
}
