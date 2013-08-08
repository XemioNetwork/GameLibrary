using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Components
{
    public class ValueProvider : IValueProvider
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueProvider"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public ValueProvider(object value)
        {
            this.Value = value;
        }
        #endregion

        #region Implementation of ILinkable<Type>
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public Type Id
        {
            get { return this.Value.GetType(); }
        }
        #endregion

        #region Implementation of IValueProvider
        /// <summary>
        /// Gets the value.
        /// </summary>
        public object Value { get; private set; }
        #endregion
    }
    public class ValueProvider<T> : IValueProvider<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueProvider{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public ValueProvider(T value)
        {
            this._value = value;
        }
        #endregion

        #region Fields
        private readonly T _value;
        #endregion

        #region IValueProvider<TValue> Member
        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value
        {
            get { return this._value; }
        }
        #endregion

        #region IValueProvider Member
        /// <summary>
        /// Gets the value.
        /// </summary>
        object IValueProvider.Value
        {
            get { return this.Value; }
        }
        #endregion

        #region ILinkable<Type> Member
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public Type Id
        {
            get { return typeof(T); }
        }
        #endregion
    }
}
