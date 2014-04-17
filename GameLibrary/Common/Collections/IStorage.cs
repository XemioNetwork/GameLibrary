using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Common.Collections
{
    public interface IStorage
    {
        /// <summary>
        /// Stores the specified value.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="value">The value.</param>
        void Store<T>(T value);
        /// <summary>
        /// Retrieves a value of the specified type.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        T Retrieve<T>();
    }
}
