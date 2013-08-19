using System;

namespace Xemio.GameLibrary.Components.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]

    public class RequireAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RequireAttribute"/> class.
        /// </summary>
        /// <param name="componentType">The component type.</param>
        public RequireAttribute(Type componentType)
        {
            if (!typeof(IComponent).IsAssignableFrom(componentType))
            {
                throw new InvalidOperationException("You can only require components that are directly inherited from IComponent.");
            }

            this.ComponentType = componentType;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type ComponentType { get; private set; }
        #endregion
    }
}
