using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Content.Layouts.Generation
{
    public class ElementTagAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementTagAttribute"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public ElementTagAttribute(string tag)
        {
            this.Tag = tag;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tag.
        /// </summary>
        public string Tag { get; private set; }
        #endregion
    }
}
