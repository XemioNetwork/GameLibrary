using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Config.Validation
{
    public class RequireComponent<T> : IValidator where T : class, IComponent
    {
        #region Implementation of ICondition
        /// <summary>
        /// Gets the default message.
        /// </summary>
        public string DefaultMessage
        {
            get { return "Component " + typeof (T) + " is required by an installer. Make sure you installed the missing component."; }
        }
        /// <summary>
        /// Gets a value indicating whether the condition is fulfilled.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public bool Validate(Configuration configuration, IComponentCatalog catalog)
        {
            return catalog.Get<T>() != null;
        }
        #endregion
    }
}
