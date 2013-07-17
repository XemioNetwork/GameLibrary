using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.UI.DataBindings
{
    public class PropertyBinding
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyBinding"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public PropertyBinding(Property source, Property destination)
        {
            this.Source = source;
            this.Destination = destination;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the source property.
        /// </summary>
        public Property Source { get; private set; }
        /// <summary>
        /// Gets the destination property.
        /// </summary>
        public Property Destination { get; private set; }
        #endregion
    }
}
