using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;

namespace Xemio.GameLibrary.Common
{
    [AbstractComponent]
    public interface IThreadInvoker : IComponent
    {
        /// <summary>
        /// Invokes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        void Invoke(Action action);
    }
}
