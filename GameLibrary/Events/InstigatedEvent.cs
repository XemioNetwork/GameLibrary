using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Events
{
    public abstract class InstigatedEvent : IInstigatedEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InstigatedEvent"/> class.
        /// </summary>
        /// <param name="instigator">The instigator.</param>
        protected InstigatedEvent(object instigator)
        {
            this.Instigator = instigator;
        }
        #endregion

        #region Implementation of IInstigatedEvent
        /// <summary>
        /// Gets the instigator.
        /// </summary>
        public object Instigator { get; private set; }
        #endregion
    }
}
