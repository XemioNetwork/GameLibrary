using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Config.Validation
{
    public class ComponentNotExists<T> : IValidator where T : IComponent
    {
        #region Implementation of IValidator
        /// <summary>
        /// Gets the default message.
        /// </summary>
        public string DefaultMessage
        {
            get { return "Component " + typeof (T) + " is required to not be existent inside the component registry."; }
        }
        /// <summary>
        /// Gets a value indicating whether the condition is fulfilled.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public bool Validate(Configuration configuration, IComponentCatalog catalog)
        {
            return catalog.Get<T>() == null;
        }
        #endregion
    }
}
