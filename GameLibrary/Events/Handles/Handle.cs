using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Events.Handles
{
    public static class Handle
    {
        #region Methods
        /// <summary>
        /// Sends the specified event to the receiver.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="evt">The event.</param>
        public static void Send(object receiver, IEvent evt)
        {
            var interfaces = Reflection.GetInterfaces(receiver.GetType())
                .Where(i => typeof(IHandle).IsAssignableFrom(i) && i.IsGenericType);

            foreach (Type interfaceType in interfaces)
            {
                Type genericType = Reflection.GetGenericArguments(interfaceType).First();
                if (genericType.IsInstanceOfType(evt))
                {
                    MethodInfo method = interfaceType.GetMethod("Handle");
                    method.Invoke(receiver, new object[] { evt });
                }
            }

            var container = receiver as IHandleContainer;
            if (container != null)
            {
                foreach (object child in container.Children)
                {
                    Handle.Send(child, evt);
                }
            }
        }
        #endregion
    }
}
