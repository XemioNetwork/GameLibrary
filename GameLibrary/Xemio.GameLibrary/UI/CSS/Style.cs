using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.UI.CSS
{
    public class Style
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Style"/> class.
        /// </summary>
        public Style()
        {
            this.Sections = new List<StyleSection>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the sections.
        /// </summary>
        public List<StyleSection> Sections { get; private set; } 
        #endregion

        #region Methods

        #endregion
    }
}
