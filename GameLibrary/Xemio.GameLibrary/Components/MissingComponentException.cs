using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Components
{
    [Serializable]
    public class MissingComponentException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingComponentException"/> class.
        /// </summary>
        /// <param name="componentType">Type of the component.</param>
        public MissingComponentException(Type componentType)
            : base(string.Format("Required component {0} doesn't exist inside the component registry."))
        {
            this.ComponentType = componentType;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingComponentException"/> class.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="componentType">Type of the component.</param>
        public MissingComponentException(Type sourceType, Type componentType)
            : base(string.Format("{0} requires a {1} component registered inside the component registry.", sourceType.Name, componentType.Name))
        {
            this.SourceType = sourceType;
            this.ComponentType = componentType;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the type of the source.
        /// </summary>
        public Type SourceType { get; private set; }
        /// <summary>
        /// Gets the type of the component.
        /// </summary>
        public Type ComponentType { get; private set; }
        #endregion
    }
}
