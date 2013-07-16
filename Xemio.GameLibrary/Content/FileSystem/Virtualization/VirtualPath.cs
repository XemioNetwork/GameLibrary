using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content.FileSystem.Virtualization
{
    public class VirtualPath : IEnumerable<string>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualPath"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public VirtualPath(string path)
        {
            this.FullPath = path;
            this._elements = this.GetElements(path);
        }
        #endregion

        #region Fields
        private readonly string[] _elements;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the full path.
        /// </summary>
        public string FullPath { get; private set; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return this._elements.LastOrDefault(); }
        }
        public int Count
        {
            get { return this._elements.Length; }
        }
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
        #endregion

        #region Methods
        /// <summary>
        /// Gets all path elements.
        /// </summary>
        /// <param name="path">The path.</param>
        private string[] GetElements(string path)
        {
            string[] pathSeparation = path.Split('/');

            return pathSeparation
                .Where(e => e != ".")
                .ToArray();
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
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<string> GetEnumerator()
        {
            return this._elements.ToList().GetEnumerator();
        }
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
