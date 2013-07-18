using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Components
{
    public interface IComponentProvider
    {
        /// <summary>
        /// Adds the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        void Add(IComponent component);
        /// <summary>
        /// Removes the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        void Remove(IComponent component);
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        void Construct();
        /// <summary>
        /// Gets a component by a specified type.
        /// </summary>
        T Get<T>() where T : IComponent;
    }
}
