﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Events;

namespace Xemio.Testing.EventSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            EventManager eventManager = new EventManager();
            eventManager.Subscribe<TestEvent>(Program.OnNext);
            eventManager.Subscribe<RedEvent>(Program.OnRed);

            eventManager.Publish(new TestEvent("Hallo Welt"));
            eventManager.Publish(new RedEvent("Hallo bloody Welt"));

            Console.ReadLine();
        }

        public static void OnNext(TestEvent value)
        {
            Console.WriteLine(value.Message);
        }

        public static void OnRed(RedEvent value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(value.Message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
