using System;

namespace Xemio.GameLibrary.Components.Attributes
{
    /// <summary>
    /// Used above interfaces that should be used as abstractions in the component manager.
    /// So every class implementing the interface will be resolvable with it.
    /// </summary>
    public class AbstractComponentAttribute : Attribute
    {
    }
}
