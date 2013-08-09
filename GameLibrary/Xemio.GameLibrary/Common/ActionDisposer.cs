using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Common
{
    public class ActionDisposer : IDisposable
    {
        #region Properties
        /// <summary>
        /// Gets the method beeing executed.
        /// </summary>
        public Action Method { get; private set; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionDisposer"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        public ActionDisposer(Action method)
        {
            this.Method = method;
        }

        #endregion Constructors

        #region Implementation of IDisposable
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            this.Method();
        }
        #endregion
    }
}
