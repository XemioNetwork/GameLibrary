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
        /// Loads the components.
        /// </summary>
        /// <param name="directory">The directory.</param>
        void LoadComponents(string directory);
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        void Finalize();
        /// <summary>
        /// Gets a component by a specified type.
        /// </summary>
        T GetComponent<T>() where T : IComponent;
    }
}
