using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Content
{
    public class ContentReference<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentReference{T}" /> class.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="fileName">Name of the file.</param>
        public ContentReference(ContentManager contentManager, string fileName)
        {
            this._contentManager = contentManager;
            this._fileName = fileName;
        } 
        #endregion

        #region Fields
        private readonly ContentManager _contentManager;
        private readonly string _fileName;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value
        {
            get { return this._contentManager.Query<T>(this._fileName); }
        }
        #endregion

        #region Operators
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="reference">The reference.</param>
        public static implicit operator T(ContentReference<T> reference)
        {
            return reference.Value;
        }
        #endregion
    }
}
