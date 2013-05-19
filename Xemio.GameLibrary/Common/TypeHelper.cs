using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common
{
    public static class TypeHelper
    {
        #region Methods
        /// <summary>
        /// Gets the base types.
        /// </summary>
        /// <param name="type">The type.</param>
        public static Type[] GetBaseTypes(Type type)
        {
            Type currentType = type;
            List<Type> types = new List<Type>();

            types.Add(currentType);            
            while (currentType.BaseType != null)
            {
                currentType = currentType.BaseType;
                types.Add(currentType);
            }

            types.AddRange(type.GetInterfaces());

            return types.ToArray();
        }
        #endregion
    }
}
