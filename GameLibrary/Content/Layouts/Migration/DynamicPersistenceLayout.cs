using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts.Migration
{
    public class DynamicPersistenceLayout<T> : PersistenceLayout<T>
    {
        #region Properties
        /// <summary>
        /// Gets the dictionary.
        /// </summary>
        public Dictionary<string, object> Dictionary { get; private set; } 
        #endregion

        #region Overrides of PersistenceLayout<T>
        /// <summary>
        /// Creates the set action.
        /// </summary>
        /// <typeparam name="TContainer">The type of the container.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="setAction">The set action.</param>
        protected override Action<object, object> CreateSetAction<TContainer, TProperty>(string tag, Action<TContainer, TProperty> setAction)
        {
            return (container, property) => this.Dictionary.Add(tag, property);
        }
        /// <summary>
        /// Creates the get action.
        /// </summary>
        /// <typeparam name="TContainer">The type of the container.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="tag">The tag.</param>
        /// <param name="getAction">The get action.</param>
        protected override Func<object, object> CreateGetAction<TContainer, TProperty>(string tag, Func<TContainer, TProperty> getAction)
        {
            return container => this.Dictionary[tag];
        }
        /// <summary>
        /// Reads properties for the specified container.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public override void Read(IFormatReader reader, object container)
        {
            this.Dictionary = new Dictionary<string, object>();
            base.Read(reader, container);
        }
        #endregion
    }
}
