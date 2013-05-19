using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Xemio.GameLibrary.Common.Link
{
    /// <summary>
    /// A class that is used to link objects to a specific value. You can resolve instances
    /// using the specified identifier defined in the ILinkable interface.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class GenericLinker<TKey, TValue> : IEnumerable<TValue> where TValue : ILinkable<TKey>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericLinker{TKey, TValue}"/> class.
        /// </summary>
        public GenericLinker()
        {
            this._linkedItems = new Dictionary<TKey, TValue>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericLinker{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public GenericLinker(Assembly assembly) : this()
        {
            this.Load(assembly);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericLinker{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public GenericLinker(IEnumerable<Assembly> assemblies) : this()
        {
            this.Load(assemblies);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericLinker{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        public GenericLinker(string assemblyName) : this()
        {
            this.Load(assemblyName);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericLinker{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public GenericLinker(IEnumerable<string> assemblies) : this()
        {
            this.Load(assemblies);
        }
        #endregion

        #region Fields
        private Dictionary<TKey, TValue> _linkedItems;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the type of the value creation.
        /// </summary>
        public CreationType CreationType { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public virtual void Add(TKey key, TValue value)
        {
            if (!this._linkedItems.ContainsKey(key))
            {
                this._linkedItems.Add(key, value);
            }
        }
        /// <summary>
        /// Adds the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        public void Add(IEnumerable<TValue> values)
        {
            foreach (TValue value in values)
            {
                this.Add(value.Id, value);
            }
        }
        /// <summary>
        /// Loads all objects, that implement the ILinkable interface out of an assembly.
        /// </summary>
        /// <param name="assembly">The assembly, that is used to load.</param>
        public void Load(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                if (typeof(TValue).IsAssignableFrom(type))
                {
                    if (!type.ContainsGenericParameters && !type.IsAbstract)
                    {
                        TValue instance = (TValue)Activator.CreateInstance(type);
                        this.Add(instance.Id, instance);
                    }
                }
            }
        }
        /// <summary>
        /// Loads the specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public void Load(IEnumerable<Assembly> assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                this.Load(assembly);
            }
        }
        /// <summary>
        /// Loads the specified assembly by a specified name.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        public void Load(string assemblyName)
        {
            this.Load(Assembly.LoadFrom(assemblyName));
        }
        /// <summary>
        /// Loads the specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public void Load(IEnumerable<string> assemblies)
        {
            foreach (string assembly in assemblies)
            {
                this.Load(assembly);
            }
        }
        /// <summary>
        /// Loads all objects, that implement the ILinkable interface out of an assembly given by a specified type name.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        public void LoadFromAssemblyOf(string typeName)
        {
            Type type = Type.GetType(typeName);
            if (type == null)
            {
                throw new InvalidOperationException(string.Format("Can't resolve the type '{0}'.", typeName));
            }

            this.Load(type.Assembly);
        }

        /// <summary>
        /// Loads all objects, that implement the ILinkable interface out of an assembly given by a specified type.
        /// </summary>
        /// <typeparam name="TType">The specified type.</typeparam>
        public void LoadFromAssemblyOf<TType>()
        {
            this.Load(typeof(TType).Assembly);
        }
        /// <summary>
        /// Resolves a given identifier and returns the stored instance, that was loaded before.
        /// </summary>
        /// <param name="identifier">Identifier for a stored instance.</param>
        /// <returns></returns>
        public TValue Resolve(TKey identifier)
        {
            TValue value = default(TValue);

            if (this._linkedItems.ContainsKey(identifier))
            {
                value = this._linkedItems[identifier];

                if (this.CreationType == CreationType.CreateNew)
                {
                    return (TValue)Activator.CreateInstance(value.GetType());
                }
            }

            return value;
        }
        #endregion

        #region Enumerator Methods
        /// <summary>
        /// Enumerates the keys.
        /// </summary>
        public IEnumerable<TKey> EnumerateKeys()
        {
            return this._linkedItems.Keys;
        }
        #endregion

        #region IEnumerable<T> Member
        /// <summary>
        /// Returns an enumerator for a specified type.
        /// </summary>
        public IEnumerator<TValue> GetEnumerator()
        {
            foreach (TValue value in this._linkedItems.Values)
            {
                yield return value;
            }
        }
        #endregion

        #region IEnumerable Member
        /// <summary>
        /// Returns an enumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
