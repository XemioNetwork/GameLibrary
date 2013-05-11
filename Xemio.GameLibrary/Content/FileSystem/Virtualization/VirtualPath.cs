using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content.FileSystem.Virtualization
{
    public class VirtualPath
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualPath"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public VirtualPath(string path)
        {
            this.FullPath = path;

            this._currentIndex = 0;
            this._elements = this.ValidatePath(path);
        }
        #endregion

        #region Fields
        private int _currentIndex;
        private readonly string[] _elements;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the full path.
        /// </summary>
        public string FullPath { get; private set; }
        /// <summary>
        /// Gets the extension.
        /// </summary>
        public string Extension
        {
            get { return Path.GetExtension(this.Name); }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is a directory.
        /// </summary>
        public bool IsDirectory
        {
            get { return !this.Name.Contains("."); }
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return this._elements[this.Count - 1]; }
        }
        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get { return this._elements.Length; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Validates the path.
        /// </summary>
        /// <param name="path">The path.</param>
        private string[] ValidatePath(string path)
        {
            return path.Split('/')
                .Where(element => element != ".")
                .ToArray();
        }
        /// <summary>
        /// Gets the next path element.
        /// </summary>
        public string MoveNext()
        {
            return this._elements[this._currentIndex++];
        }
        #endregion

        #region Object Member
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return this.FullPath;
        }
        #endregion
    }
}
