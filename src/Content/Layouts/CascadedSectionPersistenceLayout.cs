using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Content.Layouts
{
    public class CascadedSectionPersistenceLayout<T> : PersistenceLayout<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CascadedSectionPersistenceLayout{T}" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="tag">The tag.</param>
        public CascadedSectionPersistenceLayout(PersistenceLayout<T> parent, string tag)
        {
            this._parent = parent;
            this._tag = tag;
        } 
        #endregion

        #region Fields
        private readonly PersistenceLayout<T> _parent;
        private readonly string _tag;
        #endregion

        #region Methods
        /// <summary>
        /// Ends this section.
        /// </summary>
        public PersistenceLayout<T> End()
        {
            return this._parent.Add(new SectionElement<T>(this._tag, this));
        }
        #endregion
    }
}
