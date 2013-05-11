using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;

namespace Xemio.Testing.EventSystem
{
    public class RedEvent : TestEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TestEvent"/> class.
        /// </summary>
        public RedEvent(string message) : base(message)
        {
        }
        #endregion
    }
}
