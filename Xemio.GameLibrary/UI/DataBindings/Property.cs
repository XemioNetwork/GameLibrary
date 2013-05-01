using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.UI.DataBindings
{
    public class Property
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="info">The info.</param>
        public Property(object instance, PropertyInfo info)
        {
            this._instance = instance;
            this._info = info;

            this._lastValue = info.GetValue(instance, null);
        }
        #endregion

        #region Fields
        private object _lastValue;

        private readonly object _instance;
        private readonly PropertyInfo _info;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the instance.
        /// </summary>
        public object Instance
        {
            get { return this._instance; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether the property value has changed.
        /// </summary>
        public bool HasChanged()
        {
            object currentValue = this._info.GetValue(this._instance, null);
            bool hasChanged = !object.Equals(currentValue, this._lastValue);

            this._lastValue = currentValue;

            return hasChanged;
        }
        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="property">The property.</param>
        public void SetValue(Property property)
        {
            this._info.SetValue(this._instance, property.GetValue(), null);
            this._lastValue = property.GetValue();
        }
        /// <summary>
        /// Gets the value.
        /// </summary>
        public object GetValue()
        {
            return this._info.GetValue(this._instance, null);
        }
        #endregion
    }
}
