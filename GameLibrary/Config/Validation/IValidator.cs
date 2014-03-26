using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Config.Validation
{
    public interface IValidator
    {
        /// <summary>
        /// Gets the default message.
        /// </summary>
        string DefaultMessage { get; }
        /// <summary>
        /// Gets a value indicating whether the condition is fulfilled.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        bool Validate(Configuration configuration, IComponentCatalog catalog);
    }
}
