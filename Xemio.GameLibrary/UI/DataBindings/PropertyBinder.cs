using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.UI.DataBindings
{
    public class PropertyBinder
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyBinder"/> class.
        /// </summary>
        public PropertyBinder()
        {
            this._bindings = new List<PropertyBinding>();
        }
        #endregion

        #region Fields
        private List<PropertyBinding> _bindings; 
        #endregion

        #region Methods
        /// <summary>
        /// Registers the specified property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="destination">The destination.</param>
        public void Register(Property property, Property destination)
        {
            this._bindings.Add(new PropertyBinding(property, destination));
        }
        /// <summary>
        /// Updates the bindings.
        /// </summary>
        public void UpdateBindings()
        {
            foreach (PropertyBinding binding in this._bindings)
            {
                if (binding.Source.HasChanged())
                {
                    binding.Source.SetValue(binding.Destination);
                }
                if (binding.Destination.HasChanged())
                {
                    binding.Destination.SetValue(binding.Source);
                }
            }
        }
        #endregion
    }
}
