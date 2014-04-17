using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Content.Metadata
{
    public class DefaultMetadata : IMetadata
    {
        #region Constructors
        /// <summary>
        /// Provides an internal constructor for the automatic deserializer for <see cref="DefaultMetadata"/>.
        /// </summary>
        private DefaultMetadata()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMetadata" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public DefaultMetadata(Type type)
        {
            this.Guid = Guid.NewGuid();
            this.Type = type;
        }
        #endregion

        #region Implementation of IMetadata
        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        public Guid Guid { get; private set; }
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type { get; private set; }
        #endregion
    }
}
