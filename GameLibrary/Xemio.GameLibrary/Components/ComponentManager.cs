using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Plugins;

namespace Xemio.GameLibrary.Components
{
    public class ComponentManager : IComponentProvider
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentManager"/> class.
        /// </summary>
        public ComponentManager()
        {
            this._valueMappings = new Dictionary<Type, IValueProvider>();
            this.Components = new CachedList<IComponent> {AutoApplyChanges = true};
        }
        #endregion

        #region Fields
        private readonly Dictionary<Type, IValueProvider> _valueMappings;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the components.
        /// </summary>
        protected CachedList<IComponent> Components { get; private set; }
        #endregion

        #region Singleton
        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        internal static ComponentManager Instance
        {
            get { return Singleton<ComponentManager>.Value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        private void Add(Type key, IValueProvider value)
        {
            if (this._valueMappings.ContainsKey(key))
                this._valueMappings[key] = value;
            else
                this._valueMappings.Add(key, value);
        }
        /// <summary>
        /// Gets the type of the interface.
        /// </summary>
        /// <param name="component">The component.</param>
        private Type GetInterfaceType(IComponent component)
        {
            Type componentType = component.GetType();
            foreach (Type interfaceType in componentType.GetInterfaces())
            {
                if (typeof(IComponent).IsAssignableFrom(interfaceType) &&
                    typeof(IComponent) != interfaceType &&
                    typeof(IConstructable) != interfaceType)
                {
                    return interfaceType;
                }
            }

            return componentType;
        }
        /// <summary>
        /// Constructs all loaded components.
        /// </summary>
        public void Construct()
        {
            this.Components.ApplyChanges();

            foreach (IComponent component in this.Components)
            {
                IConstructable constructable = component as IConstructable;
                if (constructable != null)
                {
                    constructable.Construct();
                }
            }
        }
        #endregion

        #region IComponentProvider Member
        /// <summary>
        /// Adds the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void Add(IComponent component)
        {
            this.Components.Add(component);
            
            if (component is IValueProvider)
            {
                IValueProvider valueProvider = component as IValueProvider;
                this._valueMappings.Add(valueProvider.Id, valueProvider);

                return;
            }
            
            var value = new ValueProvider(component);
            Type interfaceType = this.GetInterfaceType(component);

            this.Add(interfaceType, value);
            if (interfaceType != component.GetType())
            {
                this.Add(component.GetType(), value);
            }
        }
        /// <summary>
        /// Removes the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void Remove(IComponent component)
        {
            this.Components.Remove(component);

            if (component is IValueProvider)
            {
                IValueProvider valueProvider = component as IValueProvider;
                this._valueMappings.Remove(valueProvider.Id);

                return;
            }

            Type interfaceType = this.GetInterfaceType(component);

            this._valueMappings.Remove(interfaceType);
            if (interfaceType != component.GetType())
            {
                this._valueMappings.Remove(component.GetType());
            }
        }
        /// <summary>
        /// Gets a component by a specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : IComponent
        {
            if (this._valueMappings.ContainsKey(typeof(T)))
            {
                return (T)this._valueMappings[typeof(T)].Value;
            }

            return default(T);
        }
        #endregion
    }
}
