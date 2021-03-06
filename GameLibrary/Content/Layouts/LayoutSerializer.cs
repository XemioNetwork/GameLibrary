﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Layouts
{
    public abstract class LayoutSerializer<T> : Serializer<T>
    {
        #region Fields
        private PersistenceLayout<T> _layout; 
        #endregion

        #region Properties
        /// <summary>
        /// Gets the layout.
        /// </summary>
        public PersistenceLayout<T> Layout
        {
            get { return this._layout ?? (this._layout = this.CreateLayout()); }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new instance inside the reading process.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected virtual T CreateInstance(IFormatReader reader)
        {
            return (T)Activator.CreateInstance(typeof(T), true);
        }
        /// <summary>
        /// Creates the layout.
        /// </summary>
        public abstract PersistenceLayout<T> CreateLayout();
        #endregion

        #region Overrides of Serializer<T>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override T Read(IFormatReader reader)
        {
            T instance = this.CreateInstance(reader);
            this.Layout.Read(reader, instance);

            return instance;
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, T value)
        {
            this.Layout.Write(writer, value);
        }
        #endregion
    }
}
