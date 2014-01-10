using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Timing;

namespace Xemio.Testing.ReactiveExtensions
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task.Factory.StartNew(async () =>
            //{
            //    double lastTick = 0;

            //    var watch = Stopwatch.StartNew();
            //    var wait = new SpinWait();

            //    while (true)
            //    {
            //        wait.SpinOnce();

            //        double sinceLastTick = watch.Elapsed.TotalMilliseconds - lastTick;
            //        Console.WriteLine(sinceLastTick);
            //        lastTick = watch.Elapsed.TotalMilliseconds;
            //    }
            //}, TaskCreationOptions.LongRunning);

            //Console.ReadLine();

            var gameLoop = new GameLoop
            {
                TargetFrameTime = 1000f/60f,
                Precision = PrecisionLevel.Highest
            };

            gameLoop.Construct();

            while (true)
            { 
                gameLoop.Run();
                Console.ReadLine();

                gameLoop.Stop();
                Console.ReadLine();
            }

            //EventManager eventManager = new EventManager();

            //eventManager.Subscribe(EventFilter<LoggingEvent>
            //                           .For(Method)
            //                           .WithCondition(f => f.Level > LoggingLevel.Information)
            //                           .Create());

            //eventManager.Publish(new LoggingEvent(LoggingLevel.Exception, "Hallo Welt"));

            //Console.ReadLine();
        }

        //private static void Method(IEvent loggingEvent)
        //{
        //    Console.WriteLine("Hallo Welt");
        //}
    }
}
