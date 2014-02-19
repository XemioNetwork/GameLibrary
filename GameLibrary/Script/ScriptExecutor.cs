using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NLog;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Events;
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
        /// <param name="evt">The evt.</param>
        /// <param name="script">The script.</param>
        public void Send(IEvent evt, IScript script)
        {
            var interfaces = Reflection.GetInterfaces(script.GetType())
                .Where(i => typeof(IHandler).IsAssignableFrom(i) && i.IsGenericType);

            foreach (Type interfaceType in interfaces)
            {
                Type genericType = Reflection.GetGenericArguments(interfaceType).First();
                if (genericType.IsInstanceOfType(evt))
                {
                    try
                    {
                        MethodInfo method = interfaceType.GetMethod("Execute");
                        method.Invoke(script, new object[] {evt});
                    }
                    catch (Exception ex)
                    {
                        logger.ErrorException("Error while executing script [" + script.Id + "]", ex);
                    }
                }
            }
        }
        /// <summary>
        /// Sends the specified event to the script.
        /// </summary>
        /// <param name="evt">The event.</param>
        /// <param name="id">The script identifier.</param>
        public void Send(IEvent evt, string id)
        {
            var implementations = XGL.Components.Require<IImplementationManager>();
            var script = implementations.GetNew<string, IScript>(id);

            if (script == null)
            {
                logger.Warn("Could not send {0} to script [{1}]. The specified script does not exist.", evt.GetType().Name, id);
                return;
            }

            this.Send(evt, script);
        }
        #endregion
    }
}
