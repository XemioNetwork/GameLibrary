using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Events.Handles
{
    public interface IHandleContainer
    {
        /// <summary>
        /// Gets an instance list containing handle implementations.
        /// </summary>
        IEnumerable Children { get; } 
    }
}
