using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Common
{
    public static class Retry
    {
        #region Methods
        /// <summary>
        /// Retries the specified action until it succeed. WARNING: dangerous.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <param name="action">The action.</param>
        public static void Unlimited(int timeout, Action action)
        {
            Retry.Unlimited<object>(timeout, () =>
            {
                action();
                return null;
            });
        }
        /// <summary>
        /// Retries the specified function until it succeed. WARNING: dangerous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static T Unlimited<T>(int timeout, Func<T> function)
        {
            while (true)
            {
                try
                {
                    return function();
                }
                catch (Exception)
                {
                    Thread.Sleep(timeout);
                }
            }
        }
        /// <summary>
        /// Retries the specified action.
        /// </summary>
        /// <param name="max">The maximum retry count.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="action">The action.</param>
        public static void Limited(int max, int timeout, Action action)
        {
            Retry.Limited<object>(max, timeout, () =>
            {
                action();
                return null;
            });
        }
        /// <summary>
        /// Retries the specified function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="max">The maximum retry count.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="function">The function.</param>
        public static T Limited<T>(int max, int timeout, Func<T> function)
        {
            int counter = 0;
            while (counter++ < max)
            {
                try
                {
                    return function();
                }
                catch (Exception)
                {
                    if (counter == max)
                        throw;

                    Thread.Sleep(timeout);
                }
            }

            return default(T);
        }
        #endregion
    }
}
