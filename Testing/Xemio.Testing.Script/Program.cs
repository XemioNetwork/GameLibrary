using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Script;

namespace Xemio.Testing.Script
{
    class Program
    {
        static void Main(string[] args)
        {
            var scriptManager = new ScriptExecutor();
            scriptManager.Send(new TriggerEvent(), "alabastia.npc1");

            Console.ReadLine();
        }
    }

    public class UselessEvent : IEvent
    {
    }

    public class TriggerEvent : IEvent
    {
    }

    public class TestScript : IScript, IHandler<TriggerEvent>
    {
        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public string Id
        {
            get { return "alabastia.npc1"; }
        }
        #endregion

        #region Implementation of IHandler<TriggerEvent>
        /// <summary>
        /// Handles the specified event.
        /// </summary>
        /// <param name="evt">The event.</param>
        public void Execute(TriggerEvent evt)
        {
            Console.WriteLine("Triggered");
        }
        #endregion
    }
}
