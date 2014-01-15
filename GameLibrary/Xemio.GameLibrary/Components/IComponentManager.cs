using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Components
{
    public interface IComponentManager
    {
        /// <summary>
        /// Constructs all loaded components.
        /// </summary>
        void Construct();
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
        /// Gets a component by a specified type.
        /// </summary>
        /// <typeparam name="T">The type of the component.</typeparam>
        T Get<T>() where T : class, IComponent;
        /// <summary>
        /// Requires the specified component.
        /// </summary>
        /// <typeparam name="T">The component type.</typeparam>
        T Require<T>() where T : class, IComponent;
    }
}
