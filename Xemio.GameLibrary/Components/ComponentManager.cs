using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
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
            this._cache = new Queue<IComponent>();
            this._valueMappings = new Dictionary<Type, IValueProvider>();
            this._componentMappings = new Dictionary<Type, IComponent>();

            this._pluginLoader = new PluginLoader<IComponent>();

            this.Components = new List<IComponent>();
        }
        #endregion

        #region Fields
        private bool _constructionMode;
        private readonly Queue<IComponent> _cache;

        private readonly Dictionary<Type, IValueProvider> _valueMappings;
        private readonly Dictionary<Type, IComponent> _componentMappings;

        private readonly PluginLoader<IComponent> _pluginLoader;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the components.
        /// </summary>
        public List<IComponent> Components { get; private set; }
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
        /// Constructs all loaded components.
        /// </summary>
        public void Construct()
        {
            this._constructionMode = true;
            {
                foreach (IComponent component in this.Components)
                {
                    IConstructable constructable = component as IConstructable;
                    if (constructable != null)
                    {
                        constructable.Construct();
                    }
                }
            }
            this._constructionMode = false;

            while (this._cache.Count > 0)
            {
                this.Add(this._cache.Dequeue());
            }
        }
        /// <summary>
        /// Loads all components found in the current directory.
        /// </summary>
        public void LoadComponents()
        {
            this.LoadComponents(".");
            this.Construct();
        }
        /// <summary>
        /// Loads all components found in the specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public void LoadComponents(string directory)
        {
            this._pluginLoader.LoadPlugins(directory);
            foreach (IComponent component in this._pluginLoader.Plugins)
            {
                this.Add(component);
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
            if (this._constructionMode)
            {
                this.AddCached(component);
                return;
            }

            if (component is IValueProvider)
            {
                IValueProvider valueProvider = component as IValueProvider;
                this._valueMappings.Add(valueProvider.Identifier, valueProvider);
            }

            this._componentMappings.Add(component.GetType(), component);
            this.Components.Add(component);
        }
        /// <summary>
        /// Adds the specified component to the component cache.
        /// </summary>
        /// <param name="component">The component.</param>
        private void AddCached(IComponent component)
        {
            this._cache.Enqueue(component);
        }
        /// <summary>
        /// Removes the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void Remove(IComponent component)
        {
            if (component is IValueProvider)
            {
                IValueProvider valueProvider = component as IValueProvider;
                this._valueMappings.Remove(valueProvider.Identifier);
            }

            this._componentMappings.Remove(component.GetType());
            this.Components.Remove(component);
        }
        /// <summary>
        /// Gets a component by a specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : IComponent
        {
            if (this._componentMappings.ContainsKey(typeof(T)))
            {
                return (T)this._componentMappings[typeof(T)];
            }

            if (this._valueMappings.ContainsKey(typeof(T)))
            {
                return (T)this._valueMappings[typeof(T)].Value;
            }

            return default(T);
        }
        #endregion
    }
}
