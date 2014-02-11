using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Content.Layouts.Generation
{
    public class TagAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TagAttribute"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public TagAttribute(string tag)
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
