using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.GameLibrary.Components
{
    public interface IValueProvider : IComponent, ILinkable<Type>
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        object Value { get; }
    }
    public interface IValueProvider<out T> : IValueProvider
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        new T Value { get; }
    }
}
