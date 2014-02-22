using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Network.Handlers.Attributes;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Handlers
{
    public abstract class ReflectedHandler<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectedServerHandler"/> class.
        /// </summary>
        protected ReflectedHandler()
        {
            this._methodCache = new Dictionary<Type, List<MethodInfo>>();

            Type type = this.GetType();
            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attributes = Reflection.GetCustomAttributes(method).OfType<T>().ToList();

                if (attributes.Count > 0)
                {
                    throw new InvalidOperationException("Handling multiple events inside one method is not supported.");
                }
                if (attributes.Count < 2 || !attributes[1].GetType().IsAssignableFrom(typeof(Package)))
                {
                    throw new InvalidOperationException("Method " + method.Name + " does not specify a package type.");
                }
                if (attributes.Count > 3)
                {
                    throw new InvalidOperationException("Invalid method signature for " + method.Name + ": Maximum number of parameters is 3.");
                }

                this.AddMethod(method, attributes.Single().GetType());
            }
        }
        #endregion

        #region Fields
        private readonly Dictionary<Type, List<MethodInfo>> _methodCache;
        #endregion

        #region Private Methods
        /// <summary>
        /// Matcheses the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="package">The package.</param>
        protected bool Matches(MethodInfo method, Package package)
        {
            return Reflection.GetParameters(method)
                             .ElementAt(1)
                             .ParameterType
                             .IsInstanceOfType(package);
        }
        /// <summary>
        /// Invokes the handlers for specified attribute type.
        /// </summary>
        /// <typeparam name="TMethod">The type of the method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        protected void Invoke<TMethod>(params object[] parameters)
        {
            this.Invoke<TMethod>(m => true, parameters);
        }
        /// <summary>
        /// Invokes the handlers for specified attribute type.
        /// </summary>
        /// <typeparam name="TMethod">The type of the method.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="parameters">The parameters.</param>
        protected void Invoke<TMethod>(Predicate<MethodInfo> predicate, params object[] parameters)
        {
            foreach (MethodInfo method in this._methodCache[typeof(TMethod)].Where(method => predicate(method)))
            {
                int parameterCount = Reflection.GetParameters(method).Length;
                if (parameterCount > parameters.Length)
                {
                    throw new ArgumentException(
                        "The method signature for " + method.Name + " provides to many parameters. Maximum number of parameters is " + parameters.Length,
                        "parameters");
                }

                object[] methodParameters = parameters.Take(parameterCount).ToArray();
                method.Invoke(this, methodParameters);
            }
        }
        /// <summary>
        /// Adds the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="attributeType">Type of the attribute.</param>
        private void AddMethod(MethodInfo method, Type attributeType)
        {
            if (!this._methodCache.ContainsKey(attributeType))
            {
                this._methodCache.Add(attributeType, new List<MethodInfo>());
            }

            this._methodCache[attributeType].Add(method);
        }
        #endregion

    }
}
