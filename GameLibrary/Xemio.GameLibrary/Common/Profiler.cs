using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Common
{
    public static class Profiler
    {
        /// <summary>
        /// Executes the given action and logs the execution time.
        /// </summary>
        /// <param name="title">The name logged with the execution time.</param>
        /// <param name="action">The action to execute.</param>
        public static void Execute(string title, Action action)
        {
            var watch = Stopwatch.StartNew();

            action();

            watch.Stop();

            Console.WriteLine("{0}: {1} ms", title, watch.Elapsed.TotalMilliseconds);
        }
    }
}
