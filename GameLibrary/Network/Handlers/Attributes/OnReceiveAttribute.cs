using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Network.Handlers.Attributes
{
    public class OnReceiveAttribute : Attribute, IServerHandlerAttribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="OnReceiveAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public OnReceiveAttribute(Type type)
        {
            this.Type = type;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type { get; private set; }
        #endregion
    }
}
