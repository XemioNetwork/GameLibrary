﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Plugins;

namespace Xemio.GameLibrary.Components
{
    public class ComponentManager
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentManager"/> class.
        /// </summary>
        public ComponentManager()
        {
            this._componentMappings = new Dictionary<Type, IComponent>();
            this.Components = new CachedList<IComponent>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<Type, IComponent> _componentMappings;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the components.
        /// </summary>
        protected CachedList<IComponent> Components { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="ComponentManager"/> is constructed.
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
            if (this._componentMappings.ContainsKey(key))
                this._componentMappings[key] = value;
            else
                this._componentMappings.Add(key, value);
        }
        /// <summary>
        /// Gets the type of the interface.
        /// </summary>
        /// <param name="type">The component type.</param>
        private IEnumerable<Type> GetInterfaceTypes(Type type)
        {
            List<Type> types = new List<Type>(from abstraction in type.GetInterfaces()
                                              where abstraction.GetCustomAttributes(typeof(AbstractComponentAttribute), false).Length > 0
                                              select abstraction);

            Type currentBaseType = type.BaseType;
            while (currentBaseType != typeof(object))
            {
                if (currentBaseType.GetCustomAttributes(typeof(AbstractComponentAttribute), false).Length > 0)
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
            object[] attributes = type.GetCustomAttributes(typeof(RequireAttribute), true);

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
            IConstructable constructable = component as IConstructable;
            if (constructable != null)
            {
                this.CheckRequiredComponents(constructable.GetType());
                constructable.Construct();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Constructs all loaded components.
        /// </summary>
        public void Construct()
        {
            if (this.IsConstructed)
                return;

            using (this.Components.StartCaching())
            { 
                foreach (IComponent component in this.Components)
                {
                    this.Construct(component);
                }
            }
        }
        /// <summary>
        /// Adds the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void Add(IComponent component)
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
        /// Gets a component by a specified type.
        /// </summary>
        /// <typeparam name="T">The type of the component.</typeparam>
        public T Get<T>() where T : class, IComponent
        {
            if (this._componentMappings.ContainsKey(typeof(T)))
            {
                return (T)this._componentMappings[typeof(T)];
            }

            return default(T);
        }
        /// <summary>
        /// Requires the specified component.
        /// </summary>
        /// <typeparam name="T">The component type.</typeparam>
        public T Require<T>() where T : class, IComponent
        {
            T component = this.Get<T>();

            if (component == null)
            {
                throw new MissingComponentException(typeof(T));
            }

            return component;
        }
        #endregion
    }
}
