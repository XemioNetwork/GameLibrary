using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Script
{
    public class DynamicScriptLoader : IConstructable, IFileSystemListener
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicScriptLoader"/> class.
        /// </summary>
        public DynamicScriptLoader() : this(".")
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicScriptLoader"/> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public DynamicScriptLoader(string directory)
        {
            this._directory = directory;
            this._compiler = new ScriptCompiler();
            this._fileMappings = new Dictionary<string, IList<IScript>>();
        }
        #endregion

        #region Fields
        private readonly string _directory;
        private readonly ScriptCompiler _compiler;
        private readonly Dictionary<string, IList<IScript>> _fileMappings;
        #endregion

        #region Methods
        /// <summary>
        /// Compiles the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private void Compile(string fileName)
        {
            var fileSystem = XGL.Components.Require<IFileSystem>();

            if (fileSystem.Path.GetExtension(fileName) != ".cs")
                return;

            Retry.Limited(10, 1000, () =>
            {
                using (Stream stream = fileSystem.Open(fileName))
                {
                    var reader = new StreamReader(stream, Encoding.Default);
                    string code = reader.ReadToEnd();

                    CompilerResult result = this._compiler.Compile(code);
                    if (result.Succeed)
                    {
                        logger.Debug("Compiled {0}.", fileSystem.Path.GetFileName(fileName));

                        this._fileMappings.Remove(fileName);
                        this._fileMappings.Add(fileName, result.Scripts);

                        var implementations = XGL.Components.Require<ImplementationManager>();
                        foreach (IScript script in result.Scripts)
                        {
                            implementations.Add<string, IScript>(script);
                        }
                    }
                }
            });
        }
        /// <summary>
        /// Initializes all existing files and compiles scripts out of them.
        /// </summary>
        /// <param name="directory">The directory.</param>
        private void Initialize(string directory)
        {
            logger.Debug("Initializing script directory {0}.", directory);

            var fileSystem = XGL.Components.Require<IFileSystem>();
            foreach (string fileName in fileSystem.GetFiles(directory))
            {
                this.Compile(fileName);
            }

            foreach (string sub in fileSystem.GetDirectories(directory))
            {
                this.Initialize(sub);
            }
        }
        #endregion

        #region Implementation of IFileSystemListener
        /// <summary>
        /// Called when a file system entry changed.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <param name="name">The name.</param>
        public void OnChanged(string fullPath, string name)
        {
            this.Compile(fullPath);
        }
        /// <summary>
        /// Called when a file system entry was created.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <param name="name">The name.</param>
        public void OnCreated(string fullPath, string name)
        {
            this.Compile(fullPath);
        }
        /// <summary>
        /// Called when a file system entry was deleted.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <param name="name">The name.</param>
        public void OnDeleted(string fullPath, string name)
        {
            if (this._fileMappings.ContainsKey(fullPath))
            {
                logger.Debug("Removing compiled script for {0}.", name);

                var implementations = XGL.Components.Require<ImplementationManager>();
                foreach (IScript script in this._fileMappings[fullPath])
                {
                    implementations.Remove<string, IScript>(script.Id);
                }

                this._fileMappings.Remove(fullPath);
            }
        }
        #endregion

        #region Implementation of IConstructable
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            var fileSystem = XGL.Components.Require<IFileSystem>();

            if (fileSystem.DirectoryExists(this._directory) == false)
            {
                logger.Warn("Script directory {0} does not exist.", this._directory);
                return;
            }

            fileSystem.Subscribe(this._directory, this);

            this.Initialize(this._directory);
        }
        #endregion
    }
}
