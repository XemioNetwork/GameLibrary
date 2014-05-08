using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Handles;
using Xemio.GameLibrary.Logging;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Script
{
    [Require(typeof(IImplementationManager))]

    public class ScriptExecutor : IComponent
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Methods
        /// <summary>
        /// Sends the specified event to the script.
        /// </summary>
        /// <param name="id">The script identifier.</param>
        /// <param name="evt">The event.</param>
        public void Send(string id, IEvent evt)
        {
            try
            {
                var implementations = XGL.Components.Require<IImplementationManager>();
                var script = implementations.GetNew<string, IScript>(id);

                if (script == null)
                {
                    logger.Warn("Could not send {0} to script [{1}]. The specified script does not exist.", evt.GetType().Name, id);
                    return;
                }

                Handle.Send(script, evt);
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error while executing script [" + id + "]", ex);
            }
        }
        #endregion
    }
}
