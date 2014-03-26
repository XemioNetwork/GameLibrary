using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Config
{
    public static class Fluently
    {
        #region Methods
        /// <summary>
        /// Returns a fluent configuration providing access to extension methods to fluently configure the XGL.
        /// </summary>
        public static FluentConfiguration Configure()
        {
            return new FluentConfiguration();
        }
        #endregion
    }
}
