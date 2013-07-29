using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Logging;

namespace Xemio.Testing.ReactiveExtensions
{
    class Program
    {
        static void Main(string[] args)
        {
            EventManager eventManager = new EventManager();

            eventManager.Subscribe(EventFilter<LoggingEvent>
                                       .Execute(Method)
                                       .When(f => f.Level > LoggingLevel.Information)
                                       .Create());
            
            eventManager.Publish(new LoggingEvent(LoggingLevel.Exception, "Hallo Welt"));

            Console.ReadLine();
        }

        private static void Method(IEvent loggingEvent)
        {
            Console.WriteLine("Hallo Welt");
        }
    }
}
