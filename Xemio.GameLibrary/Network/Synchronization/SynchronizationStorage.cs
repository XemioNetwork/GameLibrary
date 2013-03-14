using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Extensions;

namespace Xemio.GameLibrary.Network.Synchronization
{
    public class SynchronizationStorage
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizationStorage"/> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public SynchronizationStorage(ISynchronizable instance) : this(instance, Properties.Changes)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizationStorage"/> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="comparator">The comparator.</param>
        public SynchronizationStorage(ISynchronizable instance, IPropertyComparator comparator)
        {
            this._properties = new Dictionary<PropertyInfo, object>();
            this._indexMapping = new List<PropertyInfo>();

            this.Instance = instance;
            this.Instance.Synchronize(this);

            this.Comparator = comparator;
        }
        #endregion

        #region Fields
        private Dictionary<PropertyInfo, object> _properties;
        private List<PropertyInfo> _indexMapping;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the instance.
        /// </summary>
        public ISynchronizable Instance { get; private set; }
        /// <summary>
        /// Gets or sets the comparator.
        /// </summary>
        public IPropertyComparator Comparator { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Registers the specified property to the synchronization storage.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        public void Register<T>(Expression<Func<T>> expression)
        {
            if (this._indexMapping.Count + 1 >= byte.MaxValue)
            {
                throw new InvalidOperationException(
                    "The serialization system only supports a maximum of 256 properties.");
            }

            MemberExpression memberExpression = expression.Body as MemberExpression;
            PropertyInfo property = memberExpression.Member as PropertyInfo;
            
            this._properties.Add(property, null);
            this._indexMapping.Add(property);
        }
        /// <summary>
        /// Loads the property changes out of the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public void LoadChanges(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream);            
            int count = reader.ReadByte();

            for (int i = 0; i < count; i++)
            {
                bool dataWritten = reader.ReadBoolean();
                if (dataWritten)
                {
                    PropertyInfo property = this._indexMapping[i];
                    object value = reader.ReadInstance(property.PropertyType);

                    property.SetValue(this.Instance, value, null);
                }
            }            
        }
        /// <summary>
        /// Saves all property changes to the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public void SaveChanges(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write((byte)this._properties.Count);

            foreach (KeyValuePair<PropertyInfo, object> pair in this._properties)
            {
                object currentValue = pair.Key.GetValue(this.Instance, null);
                object lastValue = pair.Value;

                bool dataWritten = !this.Comparator.HasChanged(currentValue, lastValue);
                writer.Write(dataWritten);

                if (dataWritten)
                {
                    writer.WriteInstance(currentValue);
                }
            }
        }
        #endregion
    }
}
