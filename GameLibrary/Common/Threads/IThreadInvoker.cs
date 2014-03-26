using System;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;

namespace Xemio.GameLibrary.Common.Threads
{
    [Abstraction]
    public interface IThreadInvoker : IComponent
    {
        /// <summary>
        /// Invokes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        void Invoke(Action action);
    }
}
