﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Network.Handlers.Attributes
{
    public class OnBeginSendAttribute : Attribute, IServerHandlerAttribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="OnBeginSendAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public OnBeginSendAttribute(Type type)
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