using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Logging;
using Xemio.GameLibrary.Plugins;

namespace Xemio.GameLibrary.Components
{
    public class ComponentCatalog : IComponentCatalog
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentCatalog"/> class.
        /// </summary>
        public ComponentCatalog()
        {
            this._componentMappings = new Dictionary<Type, IComponent>();
            this.Components = new ProtectedList<IComponent>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<Type, IComponent> _componentMappings;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the components.
        /// </summary>
        protected ProtectedList<IComponent> Components { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="ComponentCatalog"/> is constructed.
        /// </summary>
        public bool IsConstructed { get; private set; }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        private void Add(Type key, IComponent value)
        {
            this._componentMappings[key] = value;
        }
        /// <summary>
        /// Gets the type of the interface.
        /// </summary>
        /// <param name="type">The component type.</param>
        private IEnumerable<Type> GetInterfaceTypes(Type type)
        {
            var types = new List<Type>(from abstraction in type.GetInterfaces()
                                       where abstraction.GetCustomAttributes(typeof(AbstractionAttribute), false).Length > 0
                                       select abstraction);

            Type currentBaseType = type.BaseType;
            while (currentBaseType != typeof(object))
            {
                if (Reflection.GetCustomAttributes(typeof(AbstractionAttribute)).Length > 0)
                    types.Add(currentBaseType);

                currentBaseType = currentBaseType.BaseType;
            }

            types.Add(type);

            return types.Distinct();
        }
        /// <summary>
        /// Checks for required components for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        private void CheckRequiredComponents(Type type)
        {
            var attributes = Reflection.GetCustomAttributes(type).OfType<RequireAttribute>();

            foreach (RequireAttribute attribute in attributes)
            {
                if (!this._componentMappings.ContainsKey(attribute.ComponentType))
                {
                    throw new MissingComponentException(type, attribute.ComponentType);
                }
            }
        }
        /// <summary>
        /// Constructs the specified component if it's an IConstructable.
        /// </summary>
        /// <param name="component">The component.</param>
        private void Construct(IComponent component)
        {
            var constructable = component as IConstructable;
            if (constructable != null)
            {
                this.CheckRequiredComponents(constructable.GetType());
                constructable.Construct();
            }
        }
        #endregion

        #region Implementation of IComponentManager
        /// <summary>
        /// Constructs all loaded components.
        /// </summary>
        public void Construct()
        {
            if (this.IsConstructed)
                return;

            logger.Info("Constructing components.");
            using (this.Components.Protect())
            { 
                foreach (IComponent component in this.Components)
                {
                    logger.Debug("Constructing: {0}.", component.GetType());
                    this.Construct(component);
                }
            }

            this.IsConstructed = true;
        }
        /// <summary>
        /// Adds the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void Install(IComponent component)
        {
            this.Components.Add(component);
            
            IEnumerable<Type> interfaceTypes = this.GetInterfaceTypes(component.GetType());
            foreach (Type interfaceType in interfaceTypes)
            {
                this.Add(interfaceType, component);
            }

            if (this.IsConstructed)
                this.Construct(component);
        }
        /// <summary>
        /// Removes the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void Remove(IComponent component)
        {
            this.Components.Remove(component);
            
            IEnumerable<Type> interfaceTypes = this.GetInterfaceTypes(component.GetType());
            foreach (Type interfaceType in interfaceTypes)
            { 
                this._componentMappings.Remove(interfaceType);
            }
        }
        /// <summary>
        /// Removes the specified component abstraction.
        /// </summary>
        public void Remove<T>() where T : IComponent
        {
            this.Remove(this.Get<T>());
        }
        /// <summary>
        /// Gets a component by a specified type.
        /// </summary>
        /// <typeparam name="T">The type of the component.</typeparam>
        public T Get<T>() where T : IComponent
        {
            if (XGL.State == EngineState.None)
                BasicConfigurator.Configure();

            if (this._componentMappings.ContainsKey(typeof(T)))
            {
                return (T)this._componentMappings[typeof(T)];
            }

            logger.Warn("Component {0} does not exist inside the component registry.", typeof(T).Name);

            return default(T);
        }
        /// <summary>
        /// Requires the specified component.
        /// </summary>
        /// <typeparam name="T">The component type.</typeparam>
        public T Require<T>() where T : IComponent
        {
            var component = this.Get<T>();
            if (object.Equals(component, null))
            {
                throw new MissingComponentException(typeof(T));
            }

            return component;
        }
        #endregion

        #region Implementation of IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<IComponent> GetEnumerator()
        {
            return this.Components.GetEnumerator();
        }
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
