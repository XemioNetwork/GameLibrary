using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Common
{
    public static class Profiler
    {
        #region Fields
        private static readonly Dictionary<string, int> _executionCount = new Dictionary<string, int>();
        #endregion

        #region Methods
        /// <summary>
        /// Executes the given action and logs the execution time.
        /// </summary>
        /// <param name="title">The name logged with the execution time.</param>
        /// <param name="action">The action to execute.</param>
        public static void Execute(string title, Action action)
        {
            Profiler.Execute(title, () =>
            {
                action();
                return 0;
            });
        }
        /// <summary>
        /// Executes the given function and logs the execution time.
        /// </summary>
        /// <param name="title">The name logged with the execution time.</param>
        /// <param name="function">The function to execute.</param>
        public static T Execute<T>(string title, Func<T> function)
        {
            if (!Profiler._executionCount.ContainsKey(title))
                Profiler._executionCount.Add(title, 0);

            Profiler._executionCount[title]++;

            var watch = Stopwatch.StartNew();
            var returnValue = function();
            
            watch.Stop();

            Console.WriteLine("{0}: {1} ms", title, watch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Execution count: {0}", Profiler._executionCount[title]);

            return returnValue;
        }
        #endregion
    }
}
